# 📚 Produto Solution

**Sistema de Gerenciamento de Produtos**

🎯 **Objetivo**  
Este projeto tem como objetivo criar uma solução baseada em arquitetura modular com foco em boas práticas de desenvolvimento. A aplicação possui uma **API RESTful** desenvolvida com **.NET Core** e integra-se a um banco de dados **Oracle** utilizando **Entity Framework Core** para persistência e manipulação dos dados. O Objetivo é realizar o gerenciamento de **Produtos** sabendo que cada produto contem algumas informações.

JSon
```
{
  "nome": "Smartphone Samsung",
  "descricao": "Galaxy S23 Ultra 256GB",
  "valor": 4999.99
}
```

---

## 🧱 Estrutura do Projeto

A solução está dividida em múltiplos projetos, organizados conforme a responsabilidade de cada camada:

- **ProdutoAPI** – Responsável pela exposição dos endpoints.
- **ProdutoModel** – Contém os modelos e entidades.
- **ProdutoBusiness** – Implementa as regras de negócio da aplicação.
- **ProdutoData** – Responsável pela persistência dos dados (EF Core + Oracle).

---

## 🛠️ Tecnologias Utilizadas

- **.NET Core 7**
- **Entity Framework Core**
- **Oracle Database**
- **OpenAPI (Swagger)**

---

## 🚀 Como Executar o Projeto

### 🔧 Pré-requisitos

- **.NET 7 SDK**
- **Oracle Database disponível**
- **Ferramenta para testes de API** (Postman, Insomnia, etc.)

### 🏁 Passos

1. **Clone o repositório**:

    ```bash
    git clone https://github.com/seu-usuario/projeto.git
    ```

2. **Acesse a pasta do projeto**:

    ```bash
    cd ProdutoAPI
    ```

3. **Restaure os pacotes**:

    ```bash
    dotnet restore
    ```

4. **Aplique as migrations** (caso ainda não aplicadas):

    ```bash
    dotnet ef database update --project ../ProdutoData
    ```

5. **Execute a aplicação**:

    ```bash
    dotnet run
    ```

A aplicação estará disponível para testes no endereço `http://localhost:5237/swagger`.

---

## 🧪 Testes com Postman

Você pode utilizar os seguintes endpoints para testar a API:

| Método | Endpoint         | Descrição                   |
|--------|------------------|-----------------------------|
| POST   | `/api/produto`    | Cria um novo produto        |
| GET    | `/api/produto`    | Lista todos os produtos     |
| GET    | `/api/produto/{id}` | Consulta produto específico |
| PUT    | `/api/produto/{id}` | Atualiza um produto         |
| DELETE | `/api/produto/{id}` | Remove um produto           |

---

## 📝 Licença

Este projeto é de código aberto e livre para fins educacionais e profissionais.
