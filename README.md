<p align="center" id="topo">
  <img src="./assets/logo.jpg" alt="Smart Point Administrator Logo" width="400"/>
</p>

<h1 align="center">
    Smart Point Administrator
</h1>

<p align="center"><strong>Navegação do projeto</strong></p>
<p align="center">
    <a href="#sobre">Sobre</a> |
    <a href="#funcionalidades">Funcionalidades</a> |
    <a href="#pré-requisitos">Pré-requisitos</a> |
    <a href="#tecnologias-utilizadas">Tecnologias utilizadas</a>
</p>

## Sobre

    Essa é uma API que faz integração ao projeto front-end Platform Smart Point para gestão de pontos eletrônicos online de funcionários das empresas cadastradas.

## Funcionalidades

- [x] Cadastro de usuários
- [x] Gestão de horas diárias
- [x] Gestão de horas extras
- [x] Gestão de férias
- [ ] Integração com gateway de pagamento para assinatura da plataforma
- [ ] Reconhecimento facial do funcionário
	
## Pré-requisitos

### Ferramentas

- [SDK .NET 8](https://dotnet.microsoft.com/pt-br/download)
- [Visual Studio Community 2022+](https://visualstudio.microsoft.com/pt-br/downloads/)
- [Git](https://git-scm.com/downloads)
- [PostgreSQL](https://www.postgresql.org/download/)
- [DBeaver](https://dbeaver.io/)

### Versão do DotNet

- .Net 8
- C# 12

### Start no projeto

- Abra o prompt e execute os comandos abaixo:

```bash
# Clonar o repositório do projeto(o ideal é clonar direto no c:\ para não ocorrer problemas com tamanho de diretórios muito longos)
$ git clone https://github.com/marllonramos/SmartPoint.Administrator.git

# Acessar a pasta do projeto
$ cd SmartPoint.Administrator

# Clicar na solução do projeto SmartPoint.Administrator.sln para abrir no Visual Studio Community 2022+

# No prompt do Visual Studio executar os comandos para criar as migrations(caso não existam)
dotnet ef migrations add Initial --context ApplicationDbContext --project .\src\SmartPoint.Administrator.Infra --startup-project .\src\SmartPoint.Administrator.Api --output-dir Migrations/App

dotnet ef migrations add Identity --context ApplicationIdentityDbContext --project .\src\SmartPoint.Administrator.Infra --startup-project .\src\SmartPoint.Administrator.Api --output-dir Migrations/Identity

# Comandos para criar as tabelas no banco(caso não existam)
dotnet ef database update --context ApplicationDbContext --project .\src\SmartPoint.Administrator.Infra --startup-project .\src\SmartPoint.Administrator.Api

dotnet ef database update --context ApplicationIdentityDbContext --project .\src\SmartPoint.Administrator.Infra --startup-project .\src\SmartPoint.Administrator.Api

```

## Tecnologias utilizadas

- C#
- PostgreSQL

<p align="right">
    <a href="#topo">Ir para o Topo</a>
</p>

<p align="center">
    Made by Marllon Nascimento Ramos - <a href="https://www.linkedin.com/in/marllon-ramos-6b9a2530/">meu linkedin</a>
</p>
