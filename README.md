# Tech Notes and Snippets

Mini blog feito em Blazor (Server) para listar posts de programacao a partir de um arquivo JSON.

## Visao geral

- Home lista todos os posts com titulo, data e resumo
- Tela de detalhes renderiza o conteudo do post por slug
- Dados locais em JSON, sem banco de dados
- Em andamento: consumo de API externa para buscar os posts (ainda nao implementado)

## Stack

- .NET 10 (Blazor Server)
- Razor Components
- Bootstrap (via wwwroot)

## Estrutura do projeto

- MyBlog.Web/Components/Pages: paginas Razor
- MyBlog.Web/Data: repositorio de leitura do JSON
- MyBlog.Web/Models: modelo de post
- MyBlog.Web/wwwroot/data/posts.json: base de dados local

## Como executar

1. Abra um terminal na pasta do workspace
2. Execute:

```bash
dotnet run --project MyBlog.Web
```

3. Acesse a URL exibida no terminal

## Dados de posts

Edite o arquivo abaixo para adicionar/remover posts:

- MyBlog.Web/wwwroot/data/posts.json

Campos esperados:

- slug (string)
- title (string)
- summary (string)
- publishedAt (YYYY-MM-DD)
- content (array de paragrafos)

## Rotas

- / (lista de posts)
- /posts/{slug} (detalhes do post)