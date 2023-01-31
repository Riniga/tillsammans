# tillsammans

## En plattform
För sammarbete och interaktion i ett nätverk av männskor. Utvecklat med ambition att bidra till Sveriges Idrottsföreningar och förbund.

## Funktionalitet
Dokumentation av funktionalitet finns samlad [här](documentation/requirement.md)

### En funktionalitet består av:
- Titel
- Beställare
- Användarscenario 
- status: todo, doing, done
- lösning
    - beskrivning av lösning
    - Datastruktur och lagring
    - API implmentation

### Språk
- All dokumentation, kommunikation m.m. sker för närvarande på <u>svenska</u>)>.
- Samtliga filer och kataloger är på <u>engelska</u> 
- liksom all kodning, variabelnamn, kommentarer o.s.v.

### Struktur
- All källkod samlas under katalogen "source"
- Varje tjänst skall vara så "atomär", d.v.s. så liten som möjligt och enbart tillhandahålla grundläggande funktion för sitt syfte.
- Varje tjänst beskrivs under resp. katalog med en readme.md m.m.

### Tjänster
- **nodejs** - Statisk webbplats för att tillhandahålla grafiskt gränssnitt för användarna
- **api** - C# .NET för att tillhandahålla REST API
- **library** - C# .NET bibliotek för grundläggande modeller etc
