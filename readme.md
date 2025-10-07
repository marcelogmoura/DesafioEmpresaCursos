# Desafio Técnico - Empresa de Cursos

![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)
![Database](https://img.shields.io/badge/SQL_Server-EC1B24?style=flat&logo=microsoftsqlserver&logoColor=white)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Este projeto é a implementação de uma API RESTful para um sistema de gerenciamento de **Alunos e Turmas** de uma empresa de cursos, utilizando uma arquitetura baseada em **Domain-Driven Design (DDD)**. O objetivo foi criar o CRUD (Create, Read, Update, Delete) completo, seguindo as regras de negócio complexas especificadas no desafio técnico.

O sistema foi desenvolvido como parte de um desafio técnico prático para algum processo seletivo que fiz para estudar.

## Funcionalidades Principais (CRUD Completo)

-   ?? **Cadastro de Alunos:** Permite a criação, consulta, alteração e exclusão de alunos.
-   ?? **Matrícula:** Permite associar alunos a múltiplas turmas durante o cadastro (relações N:N).
-   ?? **Gerenciamento de Turmas:** Permite a criação, consulta, alteração e exclusão de turmas.
-   ? **Documentação da API:** A API é autodocumentada utilizando Swagger (OpenAPI).

## Regras de Negócio Implementadas

O sistema segue rigorosamente as seguintes regras de negócio obrigatórias:

1.  **Matrícula Mínima:** Aluno deve ser cadastrado com no mínimo 1 turma.
2.  **Validação de Dados:** O e-mail e CPF do aluno não podem ser inválidos.
3.  **Limite de Alunos:** Uma turma não pode ter mais de 5 alunos.
4.  **Matrícula Única:** Aluno pode se matricular em diversas turmas, mas não mais de 1x na mesma turma.
5.  **Exclusão de Aluno:** Aluno não pode ser excluído se estiver associado em uma turma.
6.  **Exclusão de Turma:** Turma não pode ser excluída se possuir alunos.

## Tecnologias Utilizadas

-   **.NET 9** e **ASP.NET Core**: Framework principal para a construção da API.
-   **Entity Framework Core 9**: ORM para acesso a dados com a metodologia Code First Mapping.
-   **SQL Server**: Banco de dados relacional para persistência dos dados.
-   **DDD (Domain-Driven Design)**: Arquitetura em camadas (Domain, Infra, API).
-   **FluentValidation**: Biblioteca para validações de domínio.
-   **Docker e Docker Compose**: Para containerização da aplicação e do banco de dados.

## Pré-requisitos

-   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
-   [Docker](https://www.docker.com/products/docker-desktop/) e [Docker Compose](https://docs.docker.com/compose/install/) (para execução via container)

## Como Executar o Projeto

### 1. Usando Docker (Recomendado)

O Docker simplifica a configuração do ambiente, incluindo a base de dados SQL Server.

1.  **Clone o repositório:**
    ```bash
    git clone [URL-DO-SEU-REPOSITÓRIO]
    cd DesafioEmpresaCursos
    ```

2.  **Verifique/Edite o arquivo `docker-compose.yml`:**
    A senha padrão do SQL Server está definida como `"Your_Strong_Password_Here_!23"`. Se desejar alterá-la, edite o valor de `SA_PASSWORD` e `ConnectionStrings__DefaultConnection` no `docker-compose.yml`.

3.  **Construa e inicie os containers:**
    ```bash
    docker-compose up --build -d
    ```
    *Obs: O contêiner SQL Server pode levar alguns minutos para iniciar completamente.*

4.  **Acesse a API:**
    A aplicação estará disponível na porta `5206` do seu host.
    A documentação do **Swagger** pode ser acessada em:
    [http://localhost:5206/swagger/index.html](http://localhost:5206/swagger/index.html)

5.  **Para parar os containers:**
    ```bash
    docker-compose down
    ```

### 2. Rodando Localmente

Para executar o projeto diretamente na sua máquina, sem Docker.

1.  **Clone o repositório.**

2.  **Configure o Banco de Dados:**
    A string de conexão padrão (em `DesafioEmpresaCursos.API/appsettings.json`) está configurada para usar o LocalDB do SQL Server:
    `Server=(localdb)\\mssqllocaldb;Database=DBDesafioCurso;...`
    Certifique-se de que o SQL Server LocalDB está instalado e funcionando.

3.  **Aplique as Migrations (a partir da pasta `DesafioEmpresaCursos.Infra`):**
    ```bash
    cd DesafioEmpresaCursos.Infra
    dotnet ef database update
    ```

4.  **Execute a API (a partir da pasta `DesafioEmpresaCursos.API`):**
    ```bash
    cd ../DesafioEmpresaCursos.API
    dotnet run
    ```

5.  **Acesse a API:**
    A aplicação estará disponível em `http://localhost:5206` e o Swagger em [http://localhost:5206/swagger/index.html](http://localhost:5206/swagger/index.html).

## Estrutura do Projeto (DDD)

O projeto segue a arquitetura de Domínio-Dirigida (DDD):

-   **DesafioEmpresaCursos.Domain**: Contém as entidades de negócio (`Aluno`, `Turma`), DTOs, interfaces de repositórios e serviços, e as validações de domínio.
-   **DesafioEmpresaCursos.Infra**: Camada de acesso a dados. Contém as implementações dos repositórios (EF Core), o `AppDbContext` e os Mapeamentos (Code First).
-   **DesafioEmpresaCursos.API**: Camada de apresentação, com os controladores, injeção de dependência (`Program.cs`) e configuração do Swagger.

## Endpoints da API

A seguir, a lista de endpoints disponíveis:

| Verbo | Rota | Descrição |
| :--- | :--- | :--- |
| `POST` | `/api/Alunos` | Cadastra um novo aluno (com matrícula em turmas). |
| `GET` | `/api/Alunos` | Lista todos os alunos cadastrados. |
| `GET` | `/api/Alunos/{id}` | Obtém os detalhes de um aluno específico por ID. |
| `PUT` | `/api/Alunos/{id}` | Atualiza nome e/ou e-mail de um aluno existente (Update Parcial). |
| `DELETE` | `/api/Alunos/{id}` | Exclui um aluno (se não houver turmas associadas). |
| `POST` | `/api/Turmas` | Cadastra uma nova turma. |
| `GET` | `/api/Turmas` | Lista todas as turmas cadastradas. |
| `GET` | `/api/Turmas/{id}` | Obtém os detalhes de uma turma específica por ID. |
| `PUT` | `/api/Turmas/{id}` | Atualiza o número e/ou ano letivo de uma turma (Update Parcial). |
| `DELETE` | `/api/Turmas/{id}` | Exclui uma turma (se não houver alunos matriculados). |

---

## Autor

**Marcelo Moura**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/marcelogmoura/)
[![GitHub](https://imgshields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/marcelogmoura)