# Projectwerk_StripCatalogus

## Rolverdeling:
**Organisatie:** Verbraeken Niels  
**Testing:** Djabrailova Khava  
**Architectuur:** Lammens Frederic  
**Business** Frans Aaron

## Technisch:
* 1 solution met verschillende projecten
* Server: SQL-Server
* ADO.NET
* Github
* WPF
* Config in file
* Unit Tests
* Voldoende Documentatie

## Deel 1: StripCatalogus

| Strip               | Vb.                               |
| ------------------- |:---------------------------------:|
| Titel               | Asterix en de Gothen              |
| Reeks + nummer      | Asterix, Nr: 6                    |
| Auteurs             | Gosciny René & Uderzo Albert      |
| Uitgeverij          | Dargaud                           |

### User requirements:
1) Strips toevoegen aan catalogus
    * Hulp bij invoegen (autocomplete)
    * GEEN DUBBELS (Strips, auteurs, uitgeverij)
    * Aanpassingen strips op een gebruikvriendelijke manier
    * Aanpassingen strips => moeten eenvoudig op te zoeken zijn
2) Catalogus importeren/exporteren (JSON bestand)  

### Ontwerp:    
![ontwerp](https://user-images.githubusercontent.com/23512215/98315262-217a6b00-1fd8-11eb-8b23-df4db4980d08.jpg)

#### DomeinOntwerp:    
![DomainLayer](https://user-images.githubusercontent.com/23512215/98315258-1f181100-1fd8-11eb-951b-c39e65a0673b.jpg)

#### DataLayerOntwerp:    
![DataLayer](https://user-images.githubusercontent.com/23512215/98315235-11628b80-1fd8-11eb-9a78-047da42e7420.jpg)

#### ViewModelOntwerp :    
![ViewModel](https://user-images.githubusercontent.com/23512215/98315265-22ab9800-1fd8-11eb-92c3-a96681bfc68d.jpg)

## Deel 2: Bundels toevoegen

### Schatting refactoring:

- Analyse : 5u
  - use cases
- Ontwerp : 3u
  - uml's aanpassen.
- Datank: 30u
  - model aanpassen: 2u
  - db model updaten: 5u
  - database queries schrijven en implementeren: 13u
  - tijd voor testen : 10u
  - tijd voor eventuele probelemen die opdruiken 6u
- DomeinLaag: 14u
  - Bundel class toevoegen : 4u
  - Catalogue aanpassen: 10u
    - AddBundel: checkt of comics in lijst van comics staan
    - removeBundel moet toegevoegd worden.
    - remove comic moet aangepast worden : als in bundel enkel uit lijst van comics halen.
- UI: 16u
  - Datagrid aanpassen: 5u
  - Pagina info strips aanpassen: 8u
  - Toevoegen catalogus aanpassen: 3u
- Tests: nader te bepalen
- Powerpoint: 5u
=> Eventuele extra githubProblemen + 2u

Totaal : 75u

  
