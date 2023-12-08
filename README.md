Besvarelse af eksamensopgave for 4 semester i C# specialiseringsfaget.

Opgaven består i at:
- Modellere færger, biler og passagerer
- Tilgå en database via Entity Framwork
- Fremstille data via:
  - en API
  - en WPF applikation
  - en ASP.NET webapplikation

MULIGHEDER FOR FORBEDRINGER:
- Modellen burde indeholde en klasse til afgange/bookinger som mellemled mellem færger og biler/passagerer.
- Forretningslogikken tjekker lige nu ikke om en passager er på en anden færge ved tilføjelse
- Kun API har fuld CRUD funktionalitet til alle klasser, de to andre applikationer har kun delvis funktionalitet (til demonstration)
- Webapplikationen har ingen styling, og kan laves meget pænere
- WPF UI kan gøres pænere
- Databaseadgang kører lige nu med Entity Framework. Jeg ville foretrække at lave SQL databasen selv, og tilgå den fx via stored procedures og views.
- API'en giver offentlig adgang til redigering af alle klasser. Dette burde nok ikke være tilladt.
