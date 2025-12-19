# **[Educação Online] - Plataforma de educação online com DDD, CQRS e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **[Educação Online]**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao **MÓDULO 3 - Arquitetura, Modelagem e Qualidade de Software**.
O objetivo principal é desenvolver uma plataforma educacional online com múltiplos bounded contexts (BC), aplicando DDD, TDD, CQRS e padrões arquiteturais para gestão eficiente de conteúdos educacionais, alunos e processos financeiros.

### **Autor(es)**
- **Lucas de França Floriano**


## **2. Proposta do Projeto**

O projeto consiste em:

- **API RESTful:** Exposição dos recursos da plataforma para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e alunos.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM Entity Framework Core.
- **Testes de Integração:** Testes de integração para validar todos os principais casos de uso do sistema.
- **Testes Unitários:** Testes de unidade para validar as principais regras de negócio.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core Web API
  - Entity Framework Core
  - XUnit
- **Banco de Dados:** SQL Server/ SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:


- src/
  - Services/
    - EducacaoOnline.Api/ - API RESTful
    - EducacaoOnline.Core/ - Camada de compartilhamento de recursos
    - EducacaoOnline.Core.AntiCorruption/ - Camada anticorrupção para integração entre Bounded Contexts(BC)
      
    - EducacaoOnline.Alunos.Application/ - Camada de aplicação para o BC de gestão de Alunos
    - EducacaoOnline.Alunos.Data/ - Modelos de Dados e Configuração do EF Core
    - EducacaoOnline.Alunos.Domain/ - Camada de domínio para o BC de gestão de Alunos
    - EducacaoOnline.Alunos.Tests/ - Projeto de testes de unidade para o BC de gestão de Alunos     
    
    - EducacaoOnline.Conteudo.Application/ - Camada de aplicação para o BC de gestão de Conteúdo
    - EducacaoOnline.Conteudo.Data/ - Modelos de Dados e Configuração do EF Core
    - EducacaoOnline.Conteudo.Domain/ - Camada de domínio para o BC de gestão de Conteúdo
    - EducacaoOnline.Conteudo.Tests/ - Projeto de testes de unidade para o BC de gestão de Conteúdo   
          
    - EducacaoOnline.PagamentoFaturamento.Application/ - Camada de aplicação para o BC de gestão de Pagamento e Faturamento
    - EducacaoOnline.PagamentoFaturamento.Data/ - Modelos de Dados e Configuração do EF Core
    - EducacaoOnline.PagamentoFaturamento.Domain/ - Camada de domínio para o BC de gestão de Pagamento e Faturamento
    - EducacaoOnline.PagamentoFaturamento.Tests/ - Projeto de testes de unidade para o BC de Pagamento e Faturamento

  - IntegrationTests
    - EducacaoOnline.IntegrationTests/ - Testes de integração dos principais casos de uso do sistema
    
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **Gerenciamento de alunos:** Permite cadastrar alunos, matriculá-los, realizar aulas e finalização de cursos com emissão de certificado de conclusão.
- **Gerenciamento de conteúdo:** Permite cadastrar cursos, criar aulas e consultar seu conteúdo programático.
- **Gerenciamento de pagamento:** Permite realizar e consultar o pagamento de cursos.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.
- **Testes de Integração:** Testes de integração para validar todos os principais casos de uso do sistema.
- **Testes Unitários:** Testes de unidade para validar as principais regras de negócio.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git
- XUnit

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/lFloriano/educacao-online.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

4. **Executar a API:**
   - `cd Src/Services/EducacaoOnline.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5285/swagger/

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5285/swagger/

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
