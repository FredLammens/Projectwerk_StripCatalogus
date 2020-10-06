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
![ontwerp](https://user-images.githubusercontent.com/23512215/95026627-fd530380-0692-11eb-8243-6dbe302f6234.jpg)
#### DomeinOntwerp:    
![domein_uml](https://user-images.githubusercontent.com/23512215/95231393-635e9880-0803-11eb-967a-e5e39046423e.jpg)

#### DataLayerOntwerp:    
![datalayer_uml](https://user-images.githubusercontent.com/23512215/95231387-60fc3e80-0803-11eb-9309-e3de9a9f050a.jpg)

#### ViewModelOntwerp :    
![viewmodel_uml](https://user-images.githubusercontent.com/23512215/95231399-648fc580-0803-11eb-8ac2-bdd07875399b.jpg)


