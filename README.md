<h1 align="center"> DEVinSales - Projeto 1 - Módulo 3 - DEVinHouse</h1>

## Descrição do Projeto
<p align="left">Durante o módulo passado, criamos uma API robusta para uma aplicação de vendas, e durante as últimas semanas, aprendemos sobre tópicos avançados de .Net Core, TDD e Testes Unitários

Que tal mostrar para o time da Paradigma&Pixeon&Way2 que com o que aprendemos é possível refatorar o nosso projeto anterior, adicionando segurança e testes? Vamos melhorar a API de vendas do DEVinSales.

</p>

## Requisitos da Aplicação
<p align="left">A aplicação deve contemplar os seguintes requisitos:</p>
<ul>
    <li>
      Organização:<br>
      <ul>
          <li>Realizar fork do projeto DEVinSales (projeto 2 do módulo 2) em seu perfil do GitHub.<br>
              <pre>i. https://github.com/DEVin-Way2-Pixeon-Paradigma/M2P2-DEVinSales;</pre>
          </li>
          <li>
              Criar um quadro no Trello para mapear as tarefas a serem realizadas;
          </li>
      </ul>
    </li>
    <li>Adicionar autenticação por JWT onde só usuários logados podem acessar a aplicação.;</li>
    <li>
      Adicionar permissionamento de Administrador, Gerente e Usuário, onde:<br>
      <ul>
          <li>Administrador: Pode listar, criar, editar e deletar;</li>
          <li>Gerente: Pode listar, criar e editar;</li>
          <li>Usuário: Pode listar;</li>
      </ul>
    </li>
    <li>
      Implementação de testes:<br>
      <ul>
          <li>Implementação de mais de 30% de testes utilizando XUnit</li>
      </ul>
    </li>
</ul>

## Detalhes da implementação

<ul>
  <li>
    O uso de todos os endpoints é restrito a usuários logados, exceto os de login e cadastro de usuários.
  </li>
  <li>
    Como somente administradores (user role) são capazes de tornar outros usuários administradores, a aplicação conta com uma seed de um "super usuário".
    Credenciais de acesso para logar como admin:<br>
    <ul>
          <li>login: devinhouse@admin.com.br</li>
          <li>senha: Admin123*</li>
      </ul>
  </li>
</ul>

## 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [AspNet 6.0](https://dotnet.microsoft.com/en-us/)
- [AspNet Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio)
- [xUnit.net](https://xunit.net/)
