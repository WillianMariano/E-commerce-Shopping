# E-commerce Microservices – Projeto de Estudos .NET
Projeto de estudos desenvolvido com .NET, com o objetivo de aplicar conceitos de desenvolvimento de aplicações web, construção de Web APIs, arquitetura de microsserviços e comunicação assíncrona entre serviços.

A solução consiste em um e-commerce simplificado, composto por uma aplicação Web desenvolvida utilizando ASP.NET Core MVC e um conjunto de Web APIs organizadas em uma arquitetura de microsserviços.

### Funcionalidades
* Gerenciamento de Produtos: Web APIs responsáveis pelas operações CRUD (criação, consulta, atualização e exclusão) de produtos.
* Carrinho de Compras: API responsável pelo gerenciamento dos carrinhos e dos produtos adicionados.
* Cupons de Desconto: API para gerenciamento e aplicação de cupons de desconto.
* Ordens de Compra: API responsável pelo processamento e gerenciamento dos pedidos.
* Simulação de Pagamento: Serviço responsável por simular o processamento de pagamentos.
* Envio de E-mails: Serviço de simulação de envio de notificações por e-mail.

### Tecnologias e Conceitos Utilizados
* .NET / ASP.NET Core: Desenvolvimento da aplicação Web e das Web APIs.
* ASP.NET Core MVC: Construção da interface web do e-commerce.
* Arquitetura de Microsserviços: Organização das funcionalidades em serviços independentes.
* IdentityServer: Implementação de autenticação e autorização para a aplicação Web e as APIs.
* RabbitMQ: Implementação de comunicação assíncrona entre os microsserviços, utilizando Exchanges e mensageria para processamento de eventos relacionados às ordens de compra e ao envio de e-mails.

### Objetivos do Projeto
* O principal objetivo deste projeto é aprofundar os conhecimentos no ecossistema .NET, explorando conceitos e práticas utilizados no desenvolvimento de aplicações distribuídas, como:
* Desenvolvimento de aplicações utilizando ASP.NET Core MVC.
* Construção e integração de Web APIs.
* Arquitetura de microsserviços.
* Autenticação e autorização centralizadas.
* Comunicação assíncrona utilizando RabbitMQ.
* Processamento de mensagens e integração entre serviços.
* Separação de responsabilidades e organização da arquitetura da aplicação.
