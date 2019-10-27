README for oppgave 2 i webapps.

Innloggingsdetaljer for adminpanel:

brukernavn: admin
passord: admin
(må åpenbart endres dersom løsningen skal eksponeres mot nett)

Filsti for loggfiler:
C:\Logs\

I denne oppgaven har vi utvidet funksonaliteten som ble implementert i del 1 til å inkludere vedlikehold av data via et adminpanel som tilbyr CRUD-operasjoner mot applikasjonens database. På samme måte som i oppgave 1 har vi i stor grad basert oss på bruk av enturs public-API for å hente data som skal vises i frontend, vår database er derfor begrenset til å inneholde følgende:
		- Tabell for navn på stoppesteder som brukes i spørringer mot enturs API
		- Tabell for kundedata
		- Tabell for Kredittkort 
		- Tabell for Adminbrukere
		- Tabell for kjøpte billetter
		- Tabell for Poststeder
		- Tabell for logging av endringer i ovenstående tabeller med å overrride SaeChanges metoden

Testing:
Vi har benyttet oss av NUnit som rammeverk for enhetstesting. Merk at vi ikke har benyttet stubs for å mocke repository-laget, men istedet har brukt MOQ som rammeverk for å mocke databaseaksess. Vi har ettestrebet å implementere "rene" enhetstester, altså har vi ikke testet avhengigheter direkte i klasser som bruker disse, men har istedet testet dem i egne testklasser. Vi har oppnådd 100% testdekning for alle controller og service-klasser. Se vedlagt fil coverage.jpg som viser skjermbilde av testdekning tatt i jetbrains Rider med dotCoverage plugin.

Lagdeling:
Hele løsningen er lagdelt pr. oppgavebeskrivelsen. Dette inkluderer også de deler av løsningen som ble laget for oppgave 1.

Logging:
Alle databaseendringer logges til en egen tabell med å override SaveChanges metoden. Siden DbContexten allerede tracker hva som er endret i databasen, lagrer vi disse endringene til ChangeLogs tabellen. Så når  Øvrig logging til fil gjøres med log4net som rammeverk. Til fil logges databaseaksesser, inn/utloggseventer, og feilsituasjoner. Logfiler legges automatisk på følgende filsti: C:\Logs\. 

I denne oppgaven har vi benyttet følgende rammeverk:

	-NUnit: Enhetstesting
	-Unity: Dependency injection
	-Bootstrap: CSS/HTML framework
	-JQuery: JS framework for interakjson med DOM
	-Gijgo: JQuery plugin for dato velger
	-Cleave.js: JS framework for formatering av input felt
	-DataTable.js: JS framework for å lage søkbare og sorterbare tabeller. 
	-AutoMapper: rammeverk for automagisk mapping mellom entiteter og DTOer
	-Moq: Mocking rammeverk for testing
	