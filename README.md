# WpfDymoLabelPrinter

## Beschrijving
WpfDymoLabelPrinter is een WPF-toepassing die gebruikmaakt van de Dymo SDK om labels automatisch te printen op aangesloten Dymo-printers. Het biedt functionaliteit om wijzigingen in een labelbestand te detecteren en direct af te drukken zonder handmatige tussenkomst.

## Functionaliteiten
- **Label bestand monitoring**: Houdt wijzigingen in een specifiek labelbestand bij en start automatisch een printproces.
- **Automatisch printerbeheer**: Detecteert automatisch beschikbare Dymo-printers en stelt een standaardprinter in.
- **Label afdrukken**: Laadt een labelbestand en drukt dit af met de geselecteerde printer.

## Vereisten
- .NET 6+
- Dymo SDK
- Een compatibele Dymo-printer
- Labelbestand (`.dymo`)

## NuGet Packages
[DYMO.Connect.SDK](https://www.nuget.org/packages/DYMO.Connect.SDK/)


## Bron
Officiële Dymo Sample [Dymo DCD-SDK-Sample](https://github.com/dymosoftware/DCD-SDK-Sample)

### Auteur
Ismail El Kaddaoui
