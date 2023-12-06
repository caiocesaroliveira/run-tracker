# Cobertura de testes do projeto

O projeto conta com a implementa��o de testes de unidades. E por simplicidade
� poss�vel gerar em HTML um relat�rio de cobertura dos testes de forma a 
facilitar a verifica��o de quais partes do c�digo ainda n�o foram testados.

Pra gerar o relat�rio � necess�rio executar os testes com a coleta dos dados
de cobertura: 

```shell
dotnet test --configuration Release --blame --collect:"XPlat Code Coverage" .\RunTracker.sln
```

E ap�s os testes, deve-se gerar o relat�rio usando o comando:

```shell
reportgenerator -reports:**/TestResults/**/coverage.*.xml -targetdir:.\Domain.UnitTests\TestResults\coveragereport reporttypes:Html_Dark
```

Ficar atento a `IDENTIFICA��O` da coleta de cobertura dos testes, pois o
relat�rio em HTML � gerado a partir do resultado da an�lise da cobertura
dos testes. 