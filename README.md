How to run:
docker compose up --build

Gibt nur einen Nutzer mit GUID 2D2FAF5-1D79-403B-AD95-4417D425EBEB

Monthly Budget ist nicht implementiert

Design:
Infrastructure Project recht unnötig da ich keinen Nutzen für so ein kleines Projekt gesehen hab
API verwendet COnverter welche alle Konvertiertunsaufgaben für einen Controller/Feature zusammenfassen
Unittests hab ich mir ehrlicherweise von WindSurf generieren lassen 
Man könnte ein Generischen IRepository<TDomainObject> machen das dann von allen gebrauchten DomainObjects implementiert wird. Ich habs nicht gemacht weil alle die beiden Repos sehr unterschiedliche Funktionen haben.
