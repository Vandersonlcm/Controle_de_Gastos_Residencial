# Controle de Gastos Residenciais
 
Sistema de controle de gastos residenciais desenvolvido como projeto pessoal por
**Vanderson Luiz Cardoso Martins**.
 
A aplicação permite cadastrar pessoas de uma residência, lançar transações
financeiras (receitas e despesas) vinculadas a cada pessoa, e consultar os
totais de receitas, despesas e saldo — por pessoa e de forma geral.
 
---
 
## Sumário
 
- [Visão geral](#visão-geral)
- [Tecnologias utilizadas](#tecnologias-utilizadas)
- [Pré-requisitos](#pré-requisitos)
- [Estrutura de pastas](#estrutura-de-pastas)
- [Como executar o projeto](#como-executar-o-projeto)
  - [1. Banco de dados (MySQL)](#1-banco-de-dados-mysql)
  - [2. Back-end (ASP.NET Core API)](#2-back-end-aspnet-core-api)
  - [3. Front-end (React + TypeScript)](#3-front-end-react--typescript)
- [Endpoints da API](#endpoints-da-api)
- [Regras de negócio](#regras-de-negócio)
- [Decisões de arquitetura](#decisões-de-arquitetura)
- [Testes automatizados](#testes-automatizados)
- [Observações de segurança](#observações-de-segurança)
- [Autor](#autor)
---
 
## Visão geral
 
O sistema é dividido em duas partes que se comunicam via HTTP/REST:
 
- **Back-end**: uma Web API em **ASP.NET Core (.NET 8 / C#)**, responsável pelas
  regras de negócio e pela persistência dos dados em um banco **MySQL**.
- **Front-end**: uma aplicação **React + TypeScript** (criada com Vite), que
  consome a API para exibir e cadastrar pessoas, transações e totais.
A aplicação cobre três funcionalidades principais:
 
1. **Cadastro de pessoas** — criação, listagem e exclusão (com exclusão em
   cascata das transações da pessoa excluída).
2. **Cadastro de transações** — criação e listagem, com a regra de que pessoas
   menores de 18 anos só podem ter despesas cadastradas.
3. **Consulta de totais** — total de receitas, despesas e saldo por pessoa, e
   o total geral somando todas as pessoas.
---
 
## Tecnologias utilizadas
 
### Back-end
- **.NET 8** / **ASP.NET Core Web API**
- **C#**
- **Entity Framework Core 8** (ORM)
- **Pomelo.EntityFrameworkCore.MySql** — provedor do EF Core para MySQL
- **Swashbuckle (Swagger)** — documentação/teste interativo da API
### Front-end
- **React 19**
- **TypeScript**
- **Vite** — bundler e servidor de desenvolvimento
- **Axios** — cliente HTTP para consumir a API
- **CSS puro** (`App.css`) — estilização das telas
### Banco de dados
- **MySQL**
---
 
## Pré-requisitos
 
Antes de rodar o projeto, instale:
 
| Ferramenta | Versão recomendada | 
|---|---|
| .NET SDK | 8.0 ou superior | 
| Node.js | 18 ou superior (LTS) | 
| MySQL Server | 8.0 ou superior | 
| Ferramenta EF Core CLI | 
| Editor | Visual Studio Code |
 
### Extensões recomendadas no VS Code
 
Instale estas extensões pela aba **Extensions** (`Ctrl+Shift+X`) do VS Code:
 
1. **C# Dev Kit** (Microsoft) — suporte completo a projetos .NET/C# (IntelliSense,
   debug, executar o projeto direto pelo VS Code). Ao instalá-la, a extensão
   **C#** (base) é adicionada automaticamente como dependência.
2. **ES7+ React/Redux/React-Native snippets** — atalhos para componentes React.
3. **MySQL** (cweijan ou Jun Han) — opcional, permite visualizar/gerenciar o
   banco de dados diretamente pelo VS Code.
4. **Prettier - Code formatter** — opcional, formatação automática do código
   TypeScript/JavaScript.
Após instalar a **C# Dev Kit**, abra a pasta `ControleGastos.API` (ou o arquivo
`.sln`) no VS Code e aguarde a extensão restaurar/reconhecer o projeto antes de
tentar rodá-lo.
 
### Ferramenta `dotnet-ef` (necessária para criar as tabelas do banco)
 
O projeto já possui as *migrations* do Entity Framework Core prontas
(pasta `Migrations`), mas é preciso ter a ferramenta `dotnet-ef` instalada
globalmente para aplicá-las ao banco:
 
```bash
dotnet tool install --global dotnet-ef
```
 
---
 
## Estrutura de pastas
 
```
Controle_de_Gastos_Residencial/
├── ControleGastos.sln                  
│
├── ControleGastos.API/                 
│   ├── Controllers/
│   │   ├── PessoasController.cs        
│   │   ├── TransacoesController.cs     
│   │   └── TotaisController.cs         
│   ├── Data/
│   │   └── AppDbContext.cs             
│   ├── DTOs/
│   │   ├── PessoaDto.cs                
│   │   ├── TransacaoDto.cs             
│   │   ├── TransacaoResponseDto.cs     
│   │   └── TotaisDto.cs                
│   ├── Migrations/                    
│   ├── Models/
│   │   ├── Pessoa.cs                  
│   │   └── Transacao.cs                
│   ├── Properties/
│   │   └── launchSettings.json         
│   ├── appsettings.json                
│   ├── appsettings.Development.json
│   ├── ControleGastos.API.csproj       
│   ├── ControleGastos.API.http         
│   └── Program.cs                      
│
├── frontend/                           
│   ├── public/
│   ├── src/
│   │   ├── api/
│   │   │   └── api.ts                 
│   │   ├── components/
│   │   │   ├── Pessoas.tsx             
│   │   │   ├── Transacoes.tsx          
│   │   │   └── Totais.tsx              
│   │   ├── styles/
│   │   │   └── App.css                 
│   │   ├── App.tsx                    
│   │   └── main.tsx                    
│   ├── index.html
│   ├── package.json                    
│   ├── tsconfig.json / tsconfig.app.json / tsconfig.node.json
│   └── vite.config.ts                 
│
└── README.md                           
```
 
---
 
## Como executar o projeto
 
O projeto precisa ser executado em **3 etapas, nesta ordem**: banco de dados →
back-end → front-end.
 
### 1. Banco de dados (MySQL)
 
1. Certifique-se de que o serviço do **MySQL Server** está em execução na sua
   máquina.
2. Abra o arquivo `ControleGastos.API/appsettings.json` e ajuste a string de
   conexão `DefaultConnection` com o **usuário e senha do seu MySQL local**:
```json
   "ConnectionStrings": {
     "DefaultConnection": "server=localhost;port=3306;database=ControleGastos;user=SEU_USUARIO;password=SUA_SENHA;"
   }
```
 
   > O banco de dados `ControleGastos` **não precisa existir previamente** —
   > ele é criado automaticamente no próximo passo, ao aplicar as migrations.
 
3. Não é necessário escrever nenhum script SQL manualmente: a estrutura das
   tabelas (`Pessoas` e `Transacoes`, com a chave estrangeira e a exclusão em
   cascata) já está definida nas *migrations* do Entity Framework Core,
   dentro da pasta `ControleGastos.API/Migrations`. Essas migrations serão
   aplicadas no passo seguinte (junto com o back-end).
### 2. Back-end (ASP.NET Core API)
 
Abra um terminal na pasta `ControleGastos.API` e execute:
 
```bash
# Restaura os pacotes NuGet do projeto
dotnet restore
 
# Cria o banco de dados "ControleGastos" e as tabelas,
# aplicando as migrations existentes
dotnet ef database update
 
# Executa a API
dotnet run
```
 
Por padrão (conforme `Properties/launchSettings.json`), a API sobe em:
 
- **HTTP:** `http://localhost:5282`
- **HTTPS:** `https://localhost:7145` (perfil `https`)
- **Swagger** (documentação interativa, apenas em ambiente de desenvolvimento):
  `http://localhost:5282/swagger`
> O front-end deste projeto está configurado para chamar `http://localhost:5282/api`
> (arquivo `frontend/src/api/api.ts`), então é importante que a API esteja
> respondendo nessa porta.
 
Deixe este terminal aberto rodando a API.
 
### 3. Front-end (React + TypeScript)
 
Em **outro terminal**, na pasta `frontend`, execute:
 
```bash
# Instala as dependências do projeto
npm install
 
# Inicia o servidor de desenvolvimento (Vite)
npm run dev
```
 
O terminal exibirá o endereço local, por padrão:
 
```
http://localhost:5173
```
 
Abra esse endereço no navegador. A aplicação já deve carregar as três seções
(Pessoas, Transações e Totais) consumindo a API que está rodando na porta
`5282`.
 
> **Importante:** os dados são persistidos no MySQL, então eles continuam
> disponíveis mesmo depois de fechar e reabrir a API e o front-end — basta que
> o banco de dados MySQL continue com os mesmos dados gravados.
 
---
 
## Endpoints da API
 
Base da API: `http://localhost:5282/api`
 
### Pessoas — `/api/Pessoas`
 
| Método | Rota | Descrição | Corpo (body) |
|---|---|---|---|
| `GET` | `/api/Pessoas` | Lista todas as pessoas cadastradas | — |
| `POST` | `/api/Pessoas` | Cadastra uma nova pessoa | `{ "nome": "string", "idade": number }` |
| `DELETE` | `/api/Pessoas/{id}` | Exclui uma pessoa e, em cascata, todas as suas transações | — |
 
### Transações — `/api/Transacoes`
 
| Método | Rota | Descrição | Corpo (body) |
|---|---|---|---|
| `GET` | `/api/Transacoes` | Lista todas as transações, já com o nome da pessoa | — |
| `POST` | `/api/Transacoes` | Cadastra uma nova transação | `{ "descricao": "string", "valor": number, "tipo": "Receita" \| "Despesa", "pessoaId": number }` |
 
Validações do `POST /api/Transacoes`:
- Retorna **404** se `pessoaId` não corresponder a nenhuma pessoa cadastrada.
- Retorna **400** se a pessoa for menor de 18 anos e o `tipo` informado for `"Receita"`.
- Retorna **400** se `tipo` não for `"Receita"` nem `"Despesa"`.
### Totais — `/api/Totais`
 
| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/Totais` | Retorna o total de receitas, despesas e saldo de cada pessoa, mais o total geral |
 
Exemplo de resposta:
 
```json
{
  "pessoas": [
    {
      "pessoaId": 1,
      "nome": "Maria Silva",
      "totalReceitas": 4500.00,
      "totalDespesas": 620.50,
      "saldo": 3879.50
    }
  ],
  "totalReceitas": 4500.00,
  "totalDespesas": 620.50,
  "saldoLiquido": 3879.50
}
```
 
Você também pode testar todos os endpoints diretamente pelo **Swagger**
(`http://localhost:5282/swagger`) ou pelo arquivo `ControleGastos.API.http`
incluído no projeto (compatível com a extensão *REST Client* do VS Code).
 
---
 
## Regras de negócio
 
- **Identificadores automáticos**: os campos `Id` de `Pessoa` e `Transacao`
  são gerados automaticamente pelo banco de dados (chave primária com
  auto-incremento), não podendo ser informados manualmente no cadastro.
- **Exclusão em cascata**: ao excluir uma pessoa (`DELETE /api/Pessoas/{id}`),
  todas as transações vinculadas a ela são excluídas automaticamente. Essa
  regra é garantida no nível do banco de dados através da configuração
  `OnDelete(DeleteBehavior.Cascade)` no `AppDbContext`.
- **Menores de idade só podem ter despesas**: ao cadastrar uma transação
  (`POST /api/Transacoes`), se a pessoa vinculada tiver menos de 18 anos e o
  tipo informado for `"Receita"`, a API recusa a operação com status `400`.
- **Pessoa deve existir previamente**: uma transação só pode ser cadastrada
  para um `pessoaId` que já exista no cadastro de pessoas; caso contrário, a
  API retorna `404`.
- **Consulta de totais**: para cada pessoa, o saldo é calculado como
  `Total de Receitas − Total de Despesas`. O total geral soma os valores de
  todas as pessoas.
---
 
 
## Autor
 
Projeto pessoal desenvolvido por **Vanderson Luiz Cardoso Martins** como
teste técnico / projeto de portfólio para vaga de estágio, aplicando um
sistema completo (full stack) de controle de gastos residenciais com
ASP.NET Core, Entity Framework Core, MySQL, React e TypeScript.