# Desafio T�cnico - Empresa de Cursos

![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)
![Database](https://img.shields.io/badge/SQL_Server-EC1B24?style=flat&logo=microsoftsqlserver&logoColor=white)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Este projeto � a implementa��o de uma API RESTful para um sistema de gerenciamento de **Alunos e Turmas** de uma empresa de cursos, utilizando uma arquitetura baseada em **Domain-Driven Design (DDD)**. O objetivo foi criar o CRUD (Create, Read, Update, Delete) completo, seguindo as regras de neg�cio complexas especificadas no desafio t�cnico.

O sistema foi desenvolvido como parte de um desafio t�cnico pr�tico para algum processo seletivo que fiz para estudar.

## Funcionalidades Principais (CRUD Completo)

-   ?? **Cadastro de Alunos:** Permite a cria��o, consulta, altera��o e exclus�o de alunos.
-   ?? **Matr�cula:** Permite associar alunos a m�ltiplas turmas durante o cadastro (rela��es N:N).
-   ?? **Gerenciamento de Turmas:** Permite a cria��o, consulta, altera��o e exclus�o de turmas.
-   ? **Documenta��o da API:** A API � autodocumentada utilizando Swagger (OpenAPI).

## Regras de Neg�cio Implementadas

O sistema segue rigorosamente as seguintes regras de neg�cio obrigat�rias:

1.  **Matr�cula M�nima:** Aluno deve ser cadastrado com no m�nimo 1 turma.
2.  **Valida��o de Dados:** O e-mail e CPF do aluno n�o podem ser inv�lidos.
3.  **Limite de Alunos:** Uma turma n�o pode ter mais de 5 alunos.
4.  **Matr�cula �nica:** Aluno pode se matricular em diversas turmas, mas n�o mais de 1x na mesma turma.
5.  **Exclus�o de Aluno:** Aluno n�o pode ser exclu�do se estiver associado em uma turma.
6.  **Exclus�o de Turma:** Turma n�o pode ser exclu�da se possuir alunos.

## Tecnologias Utilizadas

-   **.NET 9** e **ASP.NET Core**: Framework principal para a constru��o da API.
-   **Entity Framework Core 9**: ORM para acesso a dados com a metodologia Code First Mapping.
-   **SQL Server**: Banco de dados relacional para persist�ncia dos dados.
-   **DDD (Domain-Driven Design)**: Arquitetura em camadas (Domain, Infra, API).
-   **FluentValidation**: Biblioteca para valida��es de dom�nio.
-   **Docker e Docker Compose**: Para containeriza��o da aplica��o e do banco de dados.

## Pr�-requisitos

-   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
-   [Docker](https://www.docker.com/products/docker-desktop/) e [Docker Compose](https://docs.docker.com/compose/install/) (para execu��o via container)

## Como Executar o Projeto

### 1. Usando Docker (Recomendado)

O Docker simplifica a configura��o do ambiente, incluindo a base de dados SQL Server.

1.  **Clone o reposit�rio:**
    ```bash
    git clone [URL-DO-SEU-REPOSIT�RIO]
    cd DesafioEmpresaCursos
    ```

2.  **Verifique/Edite o arquivo `docker-compose.yml`:**
    A senha padr�o do SQL Server est� definida como `"Your_Strong_Password_Here_!23"`. Se desejar alter�-la, edite o valor de `SA_PASSWORD` e `ConnectionStrings__DefaultConnection` no `docker-compose.yml`.

3.  **Construa e inicie os containers:**
    ```bash
    docker-compose up --build -d
    ```
    *Obs: O cont�iner SQL Server pode levar alguns minutos para iniciar completamente.*

4.  **Acesse a API:**
    A aplica��o estar� dispon�vel na porta `5206` do seu host.
    A documenta��o do **Swagger** pode ser acessada em:
    [http://localhost:5206/swagger/index.html](http://localhost:5206/swagger/index.html)

5.  **Para parar os containers:**
    ```bash
    docker-compose down
    ```

### 2. Rodando Localmente

Para executar o projeto diretamente na sua m�quina, sem Docker.

1.  **Clone o reposit�rio.**

2.  **Configure o Banco de Dados:**
    A string de conex�o padr�o (em `DesafioEmpresaCursos.API/appsettings.json`) est� configurada para usar o LocalDB do SQL Server:
    `Server=(localdb)\\mssqllocaldb;Database=DBDesafioCurso;...`
    Certifique-se de que o SQL Server LocalDB est� instalado e funcionando.

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
    A aplica��o estar� dispon�vel em `http://localhost:5206` e o Swagger em [http://localhost:5206/swagger/index.html](http://localhost:5206/swagger/index.html).

## Estrutura do Projeto (DDD)

O projeto segue a arquitetura de Dom�nio-Dirigida (DDD):

-   **DesafioEmpresaCursos.Domain**: Cont�m as entidades de neg�cio (`Aluno`, `Turma`), DTOs, interfaces de reposit�rios e servi�os, e as valida��es de dom�nio.
-   **DesafioEmpresaCursos.Infra**: Camada de acesso a dados. Cont�m as implementa��es dos reposit�rios (EF Core), o `AppDbContext` e os Mapeamentos (Code First).
-   **DesafioEmpresaCursos.API**: Camada de apresenta��o, com os controladores, inje��o de depend�ncia (`Program.cs`) e configura��o do Swagger.

## Endpoints da API

A seguir, a lista de endpoints dispon�veis:

| Verbo | Rota | Descri��o |
| :--- | :--- | :--- |
| `POST` | `/api/Alunos` | Cadastra um novo aluno (com matr�cula em turmas). |
| `GET` | `/api/Alunos` | Lista todos os alunos cadastrados. |
| `GET` | `/api/Alunos/{id}` | Obt�m os detalhes de um aluno espec�fico por ID. |
| `PUT` | `/api/Alunos/{id}` | Atualiza nome e/ou e-mail de um aluno existente (Update Parcial). |
| `DELETE` | `/api/Alunos/{id}` | Exclui um aluno (se n�o houver turmas associadas). |
| `POST` | `/api/Turmas` | Cadastra uma nova turma. |
| `GET` | `/api/Turmas` | Lista todas as turmas cadastradas. |
| `GET` | `/api/Turmas/{id}` | Obt�m os detalhes de uma turma espec�fica por ID. |
| `PUT` | `/api/Turmas/{id}` | Atualiza o n�mero e/ou ano letivo de uma turma (Update Parcial). |
| `DELETE` | `/api/Turmas/{id}` | Exclui uma turma (se n�o houver alunos matriculados). |

---

## Autor

**Marcelo Moura**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/marcelogmoura/)
[![GitHub](https://imgshields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/marcelogmoura)