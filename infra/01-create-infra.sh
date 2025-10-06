#!/usr/bin/env bash
set -e

az account show -o table

AZ_LOC="brazilsouth"
AZ_RG="rg-dimdim"
AZ_SQL_SRV="sql-dimdim$RANDOM"
AZ_SQL_ADMIN="sqladmin"
AZ_SQL_PASS="TroqueAqui!123"
AZ_DB="db-dimdim"
AZ_PLAN="plan-dimdim-linux"
AZ_WEB="app-dimdim-api"
AZ_APPINS="appi-dimdim"

echo $AZ_RG $AZ_SQL_SRV $AZ_DB $AZ_WEB

az group create -n "$AZ_RG" -l "$AZ_LOC"
az group show -n "$AZ_RG" -o table

az sql server create -g "$AZ_RG" -n "$AZ_SQL_SRV" -l "$AZ_LOC" -u "$AZ_SQL_ADMIN" -p "$AZ_SQL_PASS"

MYIP=$(curl -s https://api.ipify.org)
az sql server firewall-rule create -g "$AZ_RG" -s "$AZ_SQL_SRV" -n AllowClient --start-ip-address "$MYIP" --end-ip-address "$MYIP"
az sql server firewall-rule create -g "$AZ_RG" -s "$AZ_SQL_SRV" -n AllowAzure --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

az sql db create -g "$AZ_RG" -s "$AZ_SQL_SRV" -n "$AZ_DB" --service-objective Basic

az sql server show -g "$AZ_RG" -n "$AZ_SQL_SRV" -o table
az sql db show -g "$AZ_RG" -s "$AZ_SQL_SRV" -n "$AZ_DB" -o table

az appservice plan create -g "$AZ_RG" -n "$AZ_PLAN" --is-linux --sku B1
az webapp create -g "$AZ_RG" -p "$AZ_PLAN" -n "$AZ_WEB" --runtime "DOTNETCORE|8.0"

az webapp show -g "$AZ_RG" -n "$AZ_WEB" --query defaultHostName -o tsv

az config set extension.use_dynamic_install=yes_without_prompt extension.allow_preview=true
az extension remove -n application-insights 2>/dev/null || true
az extension add -n application-insights --upgrade
az extension list -o table

az provider register --namespace Microsoft.Insights
while [ "$(az provider show --namespace Microsoft.Insights --query registrationState -o tsv)" != "Registered" ]; do sleep 5; done

az provider register --namespace Microsoft.OperationalInsights
while [ "$(az provider show --namespace Microsoft.OperationalInsights --query registrationState -o tsv)" != "Registered" ]; do sleep 5; done
echo "Providers registrados"

AZ_LAW="law-dimdim"
az monitor log-analytics workspace create -g "$AZ_RG" -n "$AZ_LAW" -l "$AZ_LOC"
LAW_ID=$(az monitor log-analytics workspace show -g "$AZ_RG" -n "$AZ_LAW" --query id -o tsv)

az monitor app-insights component create -g "$AZ_RG" -a "$AZ_APPINS" -l "$AZ_LOC" --workspace "$LAW_ID"
AI_CONN=$(az monitor app-insights component show -g "$AZ_RG" -a "$AZ_APPINS" --query connectionString -o tsv)

az webapp config appsettings set -g "$AZ_RG" -n "$AZ_WEB" --settings "APPLICATIONINSIGHTS_CONNECTION_STRING=$AI_CONN" "ApplicationInsightsAgent_EXTENSION_VERSION=~3"
az webapp restart -g "$AZ_RG" -n "$AZ_WEB"
az webapp config appsettings list -g "$AZ_RG" -n "$AZ_WEB" -o table

SQL_CONN="Server=tcp:$AZ_SQL_SRV.database.windows.net,1433;Initial Catalog=$AZ_DB;Persist Security Info=False;User ID=$AZ_SQL_ADMIN;Password=$AZ_SQL_PASS;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
az webapp config connection-string set -g "$AZ_RG" -n "$AZ_WEB" -t SQLAzure --settings DefaultConnection="$SQL_CONN"

az webapp config connection-string list -g "$AZ_RG" -n "$AZ_WEB" -o table
az webapp show -g "$AZ_RG" -n "$AZ_WEB" --query defaultHostName -o tsv
