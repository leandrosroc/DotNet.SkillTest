# .NET SkillTest

Apresentando o projeto .NET SkillTest!
É uma solução abrangente que fornece recursos essenciais para a criação de aplicativos Web robustos e seguros. O projeto inclui autenticação JWT para proteger seus endpoints e garantir o acesso seguro do usuário. Com o Swagger, você pode gerar rapidamente a documentação da API e testar seus endpoints com facilidade.

O projeto segue o padrão Clean Architecture, que promove código modular e de manutenção. A arquitetura foi projetada para ser altamente testável, facilitando a captura de bugs no início do desenvolvimento. Também incluímos o Entity Framework, um popular mapeador relacional de objetos, para simplificar o acesso e o gerenciamento de dados.

Para garantir a compatibilidade com diferentes clientes, implementamos o suporte a CORS. Esse recurso habilita solicitações entre domínios e evita vulnerabilidades de segurança. E para lhe dar uma vantagem, incluímos um exemplo completo de operações CRUD. Este exemplo demonstra como executar operações Create, Read, Update e Delete usando a arquitetura e os recursos do projeto.

Em resumo, o projeto .NET SkillTest é uma solução abrangente e prática que fornece tudo o que você precisa para criar aplicativos Web seguros, escaláveis e de fácil manutenção.

## Configuração do Banco de Dados (Code First):
No arquivo appsettings.json, configure sua ConnectionStrings (use um banco de dados vazio, o EF code first criará as tabelas)
ou crie um novo banco de dados SQL Server com o nome: skill.db.dev

## Executar Migrations para gerar o Script do Banco de Dados SQL e criar as tabelas
No Visual Studio 2022, abra o terminal e execute:
-> dotnet tool install --global dotnet-ef
-> cd DotNet.SkillTestBack\SkillTest.API.UI
-> dotnet ef --project ..\SkillTest.Core.Infrastructures\ migrations add Users Initial --context SkillTestDBContext
-> dotnet ef database update

# Reverter Migrations 
-> dotnet ef --project ..\SkillTest.Core.Infrastructures\ migrations remove  --context SkillTestDBContext

## Iniciar a aplicação
Execute SkillTest.API.UI
Execute no ISS Express

## Swagger
Você pode acessar o Swagger em: https://localhost:44348/swagger/index.html

-> Use a API de registro para criar um novo usuário
-> Use a API de login para obter um token de acesso
-> Copie o token e insira-o nas autorizações

-> Clique no botão "autorizações" e na zona de texto "value", use o esquema "Bearer: bearer token_copiado"