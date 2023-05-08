# Csharp-Labb3-Dictionary
Lab3 – Utveckla en applikation för att träna glosor
Du får i uppdrag att utveckla ett program där man ska kunna skapa och redigera
gloslistor på olika språk, samt kunna öva på glosor från listorna man lagt in.
Ordlistorna sparas i en lokal mongoDB. Alla ordlistor sparas i samma collection.
Programmet ska finnas i två versioner: en version som körs i terminal
(consoleapp), samt en version med GUI (winforms). De båda applikationerna ska
dela kod i ett gemensamt bibliotek (class library).
Uppdragsgivaren vill även kunna använda biblioteket för ett webprojekt som de
utvecklar parallellt, och har därför strikta riktlinjer över vilka funktioner som ska
implementeras.
Class library
Implementera klasserna WordModel, WordListModel samt WordList enligt nedan:
Klassen WordModel används för att lagra ett enskilt ord och ska ha tre properties:
 public string[] Translations { get; set; }
 public int FromLanguage { get; }
 public int ToLanguage { get; }
Translations lagrar översättningarna, en för varje språk. Med FromLanguage och
ToLanguage kan man ange för övningar vilket språk som ska översättas till
respektive från. Dessa används av metoden WordList.GetWordToPractice() och
behöver inte lagras i databasen. (Read-only properties lagras inte ”by default”).
Klassen ska ha även ha konstruktor i två versioner:
public WordModel(params string[] translations)
initialiserar ’Translations’ med data som skickas in som ’translations’
public WordModel (int fromLanguage, int toLanguage,
 params string[] translations)
samma som ovan, fast sätter även FromLanguage och ToLanguage.
WordListModel
Klassen används som en datamodel för att lagra en ordlista i mongoDB.
Den ska ha följande properties:
 public Guid Id { get; set; }
 public string Name { get; set; }
 public string[] Languages { get; set; }
 public List<WordModel> Words { get; set; }
Markera property Id med [BsonId] så att mongoDB använder den som
primärnyckel. Id och namn för en ordlista tillsammans med språk, och en lista
med alla ord kommer alltså lagras i databasen.
WordList
Klassen WordList ska en private field av typen WordListModel, samt ska den
tillhandahålla propertys och metoder för att ladda in och spara listan från
mongoDB; lägga till och ta bort ord; slumpa ord för övningar; och andra metoder
beskrivet enligt nedan:
Properties:
public string Name { get {} } Namnet på listan.
public string[] Languages { get {} } Namnen på språken.
Metoder:
public Wordlist(string name, params string[] languages)
Konstruktor. Sätter properites Name och Languages till parametrarnas värden.
public static string[] GetLists()
Returnerar array med namn på alla listor som finns lagrade i databasen.
public static Wordlist LoadList(string name)
Laddar in ordlistan från databasen och returnerar som WordList. 
public void Save()
Sparar listan till databasen
public void Add(params string[] translations)
Lägger till ord i listan. Kasta ArgumentException om det är fel antal translations.
public bool Remove(int translation, string word)
translation motsvarar index i Languages. Sök igenom språket och ta bort ordet.
returnerar true om den hittade & tog bort ordet.
public int Count()
Räknar och returnerar antal ord i listan.
public void List(int sortByTranslation, Action<string[]> showTranslations)
sortByTranslation = Vilket språk listan ska sorteras på.
showTranslations = Callback som anropas för varje ord i listan.
public WordModel GetWordToPractice()
Returnerar slumpmässigt WordModel-objekt från listan, med slumpmässigt valda
FromLanguage och ToLanguage (dock inte samma).
Om det gör koden enklare / tydligare så kan du lägga till ytterligare privata
metoder i klassen. Men håll dig till de publika metoder som beskrivits ovan.
 
Console Application
Man ska kunna skapa listor, lägga till och ta bort ord, öva, m.m genom att skicka
in olika argument till programmet. Om man inte skickar några argument (eller
felaktiga argument) ska följande skrivas ut:
Use any of the following parameters:
-lists
-new <list name> <language 1> <language 2> .. <langauge n>
-add <list name>
-remove <list name> <language> <word 1> <word 2> .. <word n>
-words <listname> <sortByLanguage>
-count <listname>
-practice <listname>
-lists
Listar namnen på alla ordlistor som finns i databasen
-new <list name> <language 1> <language 2> .. <langauge n>
Skapar (och sparar) en ny lista med angivet namn och så många språk som
angivits. Går direkt in i loopen för att addera nya ord (se -add).
-add <list name>
Frågar användaren efter ett nytt ord (på listans första språk), och frågar därefter i
tur och ordning efter översättningar till alla språk i listan. Sedan fortsätter den att
fråga efter nya ord tills användaren avbryter genom att mata in en tom rad.
-remove <list name> <language> <word 1> <word 2> .. <word n>
Raderar angivna ord från namngiven lista och språk.
-words <listname> <sortByLanguage>
Listar ord (alla språk) från angiven lista. Om man anger språk sorteras listan efter
det, annars sortera efter första språket.
-count <listname>
Skriver ut hur många ord det finns i namngiven lista. 
-practice <listname>
Ber användaren översätta ett slumpvis valt ord ur listan från ett slumpvis valt
språk till ett annat. Skriver ut om det var rätt eller fel, och fortsätter fråga efter
ord tills användaren lämnar en tom inmatning. Då skrivs antal övade ord ut, samt
hur stor andel av orden man haft rätt på.
Winforms application
Uppdragsgivaren har inga specifika önskemål på hur denna utformas mer än att
den ska ha samma funktionalitet som återfinns i console appen. D.v.s det man kan
göra i console appen ska också gå att göra i GUI:t.
Tips & Hjälp
Tänk på att göra både user input och lagrad data till lower case innan du jämför
dem om du vill att ditt program ska vara case insensitive. Lagra gärna all inmatad
text i lowercase direkt i databasen.
Förslagsvis används .ReplaceOne() med ReplaceOptions IsUpsert = true för att
lagra ändringar i en ordlista till mongoDB.
Redovisning
Uppgiften ska lösas individuellt.
Lämna in uppgiften på ithsdistans med en kommentar med github-länken.
(Den som föredrar kan istället zippa mappen och ladda upp direkt på ithsdistans.)
 
Betygskriterier för godkänt:
 Class library ska innehålla metoder och properties enligt specifikation.
 Det räcker att implementera antingen konsoll-, eller winforms-appen.
 All, enligt ovan beskriven, funktionalitet ska vara på plats.
 Applikationen ska vara testad och rimligt buggfri. Be gärna någon kompis
att testa ditt program och försöka hitta buggar.
 För godkänt är det okej om man begränsar WordList så varje lista endast
har stöd för två språk. Detta kommer underlätta både datahantering, såväl
som inmatning i konsollappen och utformandet av GUI. Om man väljer att
göra detta så kan ”params”-parametern i WordList konstuktor, i Add(),
samt i Word konstruktor bytas mot två separata string-parametrar. I övrigt
ska specifikationen följas.
 WordList får inte innehålla en publik WordListModel. Tanken är att man ska
använda metoderna List() och GetWordToPractice() i applikationerna.
 Utöver att skriva själva koden, ska ni även föreslå minst 1 ny funktion /
förbättring av apparna, samt ge en rimlig tidsuppskattning för
implementation av denna. Skriv som en kommentar på uppgiften på
ithsdistans.
 Lösningen ska vara incheckad korrekt på GitHub (om ni använder GitHub)
Rootmappen ska alltså innehålla VS solution-filen (.sln) samt en mapp för
varje (3) projekt. Var och en av dessa mappar ska innehålla en VS projektfil
(.csproj) och alla projektets .cs filer (och .resx filer för winforms).
För väl godkänt krävs även:
 Både konsoll- och winforms-versionen av apparna är implementerade.
 Att man tänkt igenom användargränsnittet (winforms) så att det känns
användbart och enkelt att jobba med.
 Koden ska vara väl strukturerad och lätt att förstå.
 WordList har stöd för godtyckligt antal språk, enligt spec.
 Föreslå istället minst 3 nya funktioner / förbättringar samt ge en rimlig
tidsuppskattning för implementation av vardera. Skriv som en kommentar
på uppgiften på ithsdistans. 
