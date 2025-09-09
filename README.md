# 🚀 Teste Bellosoft - API .NET 8 com integração NASA APOD

## 📖 Visão Geral
Este projeto foi desenvolvido como parte de um desafio técnico para Backend Developer Junior. A aplicação é uma **API REST em .NET 8** que consome a [NASA Astronomy Picture of the Day (APOD)](https://api.nasa.gov/) e persiste os dados em um banco **SQL Server**.

Funcionalidades principais:
- Buscar o **APOD do dia atual** via NASA API (`POST /api/nasaapod/today`)
- Persistir no banco apenas os campos relevantes (`Date`, `Title`, `Url`)
- Listar **todos os APODs salvos** no banco (`GET /api/nasaapod/all`)
- Pesquisar APOD salvo por **data específica** (`GET /api/nasaapod/by-date?date=yyyy-MM-dd`)

---

## 🛠️ Tecnologias Utilizadas
- **.NET 8 (ASP.NET Core Web API)**
- **Entity Framework Core 8** (Code First + SQL Server)
- **Swagger/OpenAPI** (documentação da API)
- **HttpClient** para integração com a API da NASA

---

## ⚙️ Instruções de Configuração

Siga os passos abaixo para configurar e executar o projeto em seu ambiente de desenvolvimento local.

### Pré-requisitos
- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
- **[SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)**

### 1. Obtenha o Código do Projeto
Primeiro, clone ou faça o download do código-fonte a partir da página principal do repositório no GitHub para o seu computador. Em seguida, navegue para a pasta que foi criada.

### 2. Configure a Conexão com o Banco de Dados
A aplicação precisa saber como se conectar ao seu SQL Server. Para isso, você deve editar o arquivo de configurações.
Abra o arquivo chamado **appsettings.json**, que está na raiz do projeto. Dentro dele, localize a seção **ConnectionStrings** e encontre a chave **DefaultConnection**.
Você precisará alterar o valor dessa chave para que aponte para o seu servidor. A configuração deve incluir o endereço do seu servidor (Server), o nome do banco de dados que será criado (Database=NasaApodDB) e suas credenciais de acesso (seja usando `Trusted_Connection=True` para autenticação do Windows ou `User Id` e `Password` para autenticação do SQL Server). Lembre-se também de incluir `TrustServerCertificate=True` no final da string.

### 3. Crie o Banco de Dados
Com a conexão configurada, o Entity Framework Core criará a estrutura do banco de dados para você.
Abra um terminal ou prompt de comando na pasta raiz do projeto e execute o comando: **dotnet ef database update**.
Após a execução, o banco de dados `NasaApodDB` e suas tabelas estarão prontos no seu SQL Server.

### 4. Execute a Aplicação
Agora, você pode iniciar a API. No mesmo terminal, na raiz do projeto, execute o comando: **dotnet run**.
O terminal irá informar que a aplicação está em execução e mostrará os endereços para acessá-la.

### 5. Acesse a Documentação da API (Swagger)
Para visualizar e testar os endpoints da API, abra seu navegador e acesse a documentação do Swagger. O endereço será algo como **https://localhost:PORTA/swagger/index.html**, substituindo `PORTA` pelo número da porta informado no terminal quando você executou a aplicação.
