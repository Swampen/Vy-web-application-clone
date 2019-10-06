README for oppgave 1 i webapps.

Vi har i denne oppgaven laget en web-applikasjon som etterlikner Vy.no sin løsning for kjøp av billetter. Vi planla til å begynne med å lage en større database for å håndtere strekninger, avganger og priser, men skrinla dette da vi fant ut av at vi kunne benytte oss av et public API fra vy som gav oss all denne dataen. Lagring av kundedata, billetter og betalingsinformasjon gjøres ved hjelp av en datbasestruktur opprettet med Entity-framework code first. 

Om aplikasjonens arkitektur:

I grove trekk er vår løsning bygget opp som beskrevet under:

	-Vi har en lagdelt struktur der de viktigste komponenter er som følger:
		
		-Database lag, implementert med entity frameweork
			
			-Entitetsklasser som mapper tabellene i databasen

		-Repository lag. Alle CRUD operasjoner mot databasen skjer via dette laget

		- Service lag. Dette laget er bindeleddet mellom controller og repository. Her gjøres mapping mellom entiteter og "flate" serialiserbare data-transfer-objekter.

		- Controller lag. Endepunkter som eksponerer backend funksjonalitet mot frontend. 

		-View / Client lag. Client-side kode som inneholder all frontend-funksjonalitet.

	- For å underlette testing og redusere kompleksitet knyttet til avhengigheter mellom klassene har vi benyttet oss av Unity for å håndtere dependency-injections. Dette tilater oss å binde opp Interface mot konkrete implementasjoner, og enkelt endre disse ved behov.


I denne oppgaven har vi benyttet følgende rammeverk:

	-NUnit: Enhetstesting
	-Unity: Dependency injection
	-Bootstrap: CSS/HTML framework
	-JQuery: JS framework for interakjson med DOM
	-Gijgo: JQuery plugin for dato velger
	-Cleave.js: JS framework for formatering av input felt
	-AutoMapper: rammeverk for automagisk mapping mellom entiteter og DTOer
	-Moq: Mocking rammeverk for testing