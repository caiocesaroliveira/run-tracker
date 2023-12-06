# Cobertura de testes do projeto

O projeto conta com a implementação de testes de unidades. E por simplicidade
é possível gerar em HTML um relatório de cobertura dos testes de forma a 
facilitar a verificação de quais partes do código ainda não foram testados.

Pra gerar o relatório é necessário executar os testes com a coleta dos dados
de cobertura: 

```shell
dotnet test --configuration Release --blame --collect:"XPlat Code Coverage" .\RunTracker.sln
```

E após os testes, deve-se gerar o relatório usando o comando:

```shell
reportgenerator -reports:**/TestResults/**/coverage.*.xml -targetdir:.\Domain.UnitTests\TestResults\coveragereport reporttypes:Html_Dark
```

Ficar atento a `IDENTIFICAÇÃO` da coleta de cobertura dos testes, pois o
relatório em HTML é gerado a partir do resultado da análise da cobertura
dos testes. 