# Nodejs-Backend-CRUD

Este é um projeto dotnet 7.0 que utiliza Microsoft.EntityFrameworkCore.InMemory para criar e gerenciar um banco de dados em memória. Ele inclui um Swagger Page para documentação da API.

O sistema tem um mecanismo de login usando JWT, com um entrypoint que recebe { "login":"letscode", "senha":"lets@123"} e gera um token.

## Pré-requisitos

Certifique-se de ter o seguinte instalado em seu sistema:

SDK do .NET 7:
Você precisará instalar o SDK (Software Development Kit) do .NET 7 em seu sistema. O SDK contém as ferramentas necessárias para compilar, depurar e executar aplicativos .NET.
IDE (Ambiente de Desenvolvimento Integrado):
IDEs para desenvolvimento com .NET, como o Visual Studio, Visual Studio Code ou JetBrains Rider.

-   [.Net SDK 7]([https://nodejs.org/](https://dotnet.microsoft.com/en-us/download/dotnet/7.0))
-   [Visual Studio Code]([https://www.npmjs.com/](https://code.visualstudio.com/download)) (IDE para desenvolvimento)

## Instalação

1. Clone este repositório:

    ```bash
    git clone https://github.com/gabrielportuga/csharp-backend-crud.git
    ```

2. Navegue até o diretório do projeto:

    ```bash
    cd csharp-backend-crud
    ```

3. Restaura as dependências do projeto:
    
    ```bash
    dotnet restore
    ```


## Uso

Para compilar o projeto:

```bash
dotnet build 
```

Para iniciar o servidor da aplicação, execute o seguinte comando:

```bash
dotnet run 
```

Isso iniciará o servidor na porta padrão 5000. Você pode acessar a API em `http://localhost:5000`.

## Documentação da API (Swagger)

A documentação da API está disponível e pode ser acessada na seguinte URL:

```
http://localhost:5000/swagger/
```
