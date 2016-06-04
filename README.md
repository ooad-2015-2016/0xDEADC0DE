![0xdeadc0de](https://raw.githubusercontent.com/ooad-2015-2016/0xDEADC0DE/master/header.png)

# 0xDEADC0DE

## fleet-tracker

### Članovi tima

* Parić Muhamed
* Polić Muamer
* Popović Ahmed
* Ramić Benjamin

### Opis teme
---

Softversko rješenje koje omogućava kompanijama koje se bave prevozničkim djelatnostima korisno i lahko praćenje njihovih vozila na terenu. Poseban fokus je na gradskom saobraćaju i špedicijama, gdje će osim administratora iz kompanije, mogućnost reduciranog trackinga (samo osnovnih podataka o vozilima) imati i korisnici usluga određene kompanije. Naime, korisnici usluga kompanije će moći dobiti informacije na kojem mjestu se nalazi tramvaj, autobus, trolejbus ili kamion kojeg čekaju, za koliko vremena će doći do sljedeće stanice (ili mjesta na kojoj se nalazi korisnik) i slične informacije.

Kompanije i vlasnici software-a su u mogućnosti da nadziru sva svoja vozila u svakom trenutku, tj. da ih lociraju na karti, da odrede da li je vozilo u pokretu ili ne, da li je vozilo stiglo na vrijeme, da li je vozilo pratilo određenu rutu, koliko vremena je voženo, koliko vremena je vozilo provelo ne krećući se, prosječnu brzinu i slično.

Korisnici ovog sistema (vlasnici kompanija) imaju olakšan nadzor, uvid i statistiku svojih vozila. Sistem poslovanja sa ovim software-om se poboljšava na način da će korisnici moći lakše da obavljaju poslove izdavanja naloga, administracije vožnji/pošiljki, nadzora…

### Procesi
---

#### Registracija i prijava u sistem

Korisnik je odlaskom na index stranicu prezentovan sa mogućnošću prijave i registracije u sistem. Registracija se vrši upisom podataka u formu (ime, prezime, email, lozinka), međutim korisnik se ne može prijaviti dok ga ne verificira globalni administrator. Globalni administrator dobija email po zathjevu za registraciju korisnika, te onda u admin panelu vrši verifikaciju, dodjeljivanje nivoa pristupa novoverifikovanom korisniku te dodavanje tog korisnika u neku od postojećih grupa. Kada je korisnik verifikovan i kada je dodjeljen grupi, dobija email da se sada može prijaviti u sistem, jednostavnim upisivanjem email-a i lozinke u formu za prijavu.


#### Organizacija vozača, vozila i ruta

Omogućeno je dodavanje, brisanje i izmjena ruta, vozača, vozila i ujedno i dodjeljivanje dostupnih tracking uređaja vozilima kroz jednostavan interfejs. 

#### Kreiranje naloga vožnje / instanci ruta

Odlaskom na podstranicu za kreiranje naloga, administrator-klijent izabira podatke potrebne za kreiranje nove instance rute (svi ovi podaci su prethodno uneseni u sistem kroz organizaciju ruta i vozila): ruta (polazište i dolazište), vozilo, komentar, očekivano vrijeme polaska, očekivano vrijeme dolaska. Po jednostavnoj kreaciji naloga vožnje/instance rute, tracking vozila će biti omogućen.


#### Kretanje vozila sa uključenim tracking uređajem povezanim sa sistemom
Po uključenju tracking uređaja unutar vozila, uređaj se povezuje sa serverom i šalje paket da GPS tracking počinje. Server svakih 15s prima novi paket od uređaja, sa njegovom geografskom širinom i dužinom, ID-om uređaja, te snima svaki paket u databazu i povezuje ga sa vozilom, te omogućava tracking prijavljenim administrator-klijentima putem sistema ili neprijavljenim/neregistrovanim korisnicima, ukoliko je grupa kojoj vozilo pripada označena kao javna.

#### Pregled detaljnih podataka i detaljne statistike
Omogućen je pregled individualne i globalne statistike za rute, vozila i vozače unutar jedne grupe. Prikazani su detalji poput ukupnog broja pređenih kilometara za vozilo tokom trackinga, prosječno vrijeme kašnjenja transporta na rutama, provedeno vrijeme vozila u pauzama (kada se vozilo nije kretalo) i slično. Naravno, omogućen je i pregled liste unesenih vozila, vozača, ruta i podataka vezanih za njih.


#### Pregled najbitnijih podataka od strane neregistrovanih korisnika

Neregistrovani (ili neprijavljeni) korisnici će biti u mogućnosti da putem web interfejsa sistema ili mobilne aplikacije izaberu grupe koje su se označile kao javne, te da vide njihova vozila na mapi, sa procjenjenim vremenom dolaska do lokacije korisnika (ili neke druge tačke od interesa).


#### Omogućavanje korištenja servisa sistema
Sistem fleet tracking je SaaS, dakle korisnik ovog sistema neće brinuti o tehničkom održavanju sistema (morat će samo brinuti o tracking GPS uređaju), budući da će sistem biti centralno hostan kod kreatora (0xDEADC0DE), gdje će samo kreatori imati pristup korištenoj tehnologiji. To naravno podrazumijeva da ce 0xDEADC0DE vršiti svo otklanjanje eventualnih problema i generalno održavanje sistema. Dakle, eliminisana je bespotrebna uspostava (setup) sistema odvojeno kod svakog klijenta što bi podrazumijevalo i predavanje tehnologije (koda), već će klijentima biti omogućeno korištenje sistema kroz registraciju korisničkih naloga, te njihovu verifikaciju i kreiranje grupe od strane globalnih administratora.

### Funkcionalnosti
---

* Mogućnost kreiranja novih grupa
* Mogućnost dodavanja novih administrator-klijenata grupama
* Mogućnost dodavanja novih vozila, vozača i ruta grupi
* Mogućnost praćenja geografske lokacije vozila i relevantnih podataka u realnom vremenu, pod uslovom da korisnik ima odgovarajuće privilegije
* Mogućnost filtriranja praćenih vozila po tipu i ruti
* Administrator-klijenti imaju mogućnost praćenja statistike svakog vozila u svojoj grupi (vrijeme provedeno na putu, prosječna brzina, itd.)

### Akteri
---

* **Globalni administratori** su osobe koje su vlasnici softverskog rješenja, u ovom slučaju njegovi kreatori (članovi tima 0xDEADC0DE). 
  * Imaju account (e-mail, password)
  * Kreiraju grupe i druge globalne administratore
  * Unose administrator - klijente unutar grupe 
  * Imaju sve privilegije/role kao i administrator-klijenti
  * Dodjeljuju administrator - klijente grupama

* **Grupe** su odvojene nezavisne cjeline (kompanije) koje koriste softversko rješenje i može ih biti neograničen broj. Unutar jedne grupe se odvija jedan poslovni proces. Grupe mogu biti različitih tipova (špedicije, autoprevoznici..)
  * Mogu biti privatne i javne
  * Posjeduju određen broj ravnopravnih administrator-klijenata
  * Posjeduju određen broj vozila
  * Posjeduju određen broj vozača
  * Bave se sebi specifičnom vrstom prijevoza
  * Imaju određen broj tracking uređaja koji su im dodjeljeni

* **Administrator-klijenti** su osobe koje su uposlenici određenih kompanija (grupa) koji učestvuju u procesu poslovanja određene grupe (kompanije). Administrator-klijenti na nivou jedne kompanije imaju iste privilegije.
  * Imaju account (e-mail, password)
  * Unose vozila (tip [tramvaj, voz, trolejbus, bus, kamion, kombi, lični automobil], model [VW, Tatra …], registracijski broj, broj šasije...)
  * Unose vozače (ime, prezime, JMBG)
  * Unose rute vozila
  * Kreiraju naloge (instance rute)
  * Prate vozila kompanije (grupe)
    * Snimanje pozicije (geo. širina, dužina) i vremena snimanja pozicije u trenutnoj ruti - u databazu - efektivno pamćenje čitavih ruta)
    * Početak rute (uključivanje uređaja), kraj rute (isključivanje uređaja)
  * Imaju prikaz praćenja svojih vozila
    * Mapu sa prikazanim svim vozilima
    * Mapa sa prikazanim jednim vozilom
      * uz statistiku (pređeni kilometri, prosječna brzina)

* **Putnik/čovjek koji čeka robu/čovjek koji čeka tramvaj** je osoba koja koristi usluge određene grupe i koja želi da zna na kojem mjestu se nalazi njegova pošiljka, ili na kojem mjestu je sljedeći autobus..
  * Nema account
  * Ima mapu sa prikazanim vozilima od interesa
    * Očekivano vrijeme dolaska
    * Sljedeće vozilo od interesa koje dolazi 

* **Eksterni (GPS) uređaj** (mobitel) omogućava interakciju softvera sa vozilom na terenu. Šalje svoje koordinate na server i komunicira sa softverom.
  * Ima svoj unikatni identifikator
  * Evidentiran je u databazi i povezan je sa određenom grupom
  * Komunicira sa serverom putem HTTP protokola i šalje trenutnu lokaciju i svoj ID


### Posljednji commit

* Koristi se MSSql databaza koja je hostana na azure, dakle, remote. (http://fleet-tracker.azurewebsites.net/)
  * Preporucujemo testiranje aplikacije na azurewebsites, mnogo je lakše nego cijeli projekat dizati na svom lokalnom računaru. Također, aplikacija je rađena na engleskom jeziku, ne treba Vas to zbunjivati, nama je bilo lakše tako. Help je ipak urađen na Bosanskom jeziku.
* Eksterni uređaj koji se koristi je GPS (simulator i mobitel) i koristi se unutar klase ticksApiController (https://github.com/ooad-2015-2016/0xDEADC0DE/blob/master/fleet-tracker/fleet-tracker/Controllers/TicksApiController.cs)
* Sva validacija je ispoštovana kroz CRUD koji obezbjeđuje ASP.NET (https://github.com/ooad-2015-2016/0xDEADC0DE/tree/master/fleet-tracker/fleet-tracker/Controllers)
* Koriste se google mape na index stranici, kao i servisi za geo lokaciju unutar UWP (https://github.com/ooad-2015-2016/0xDEADC0DE/tree/master/GPSMobilnaAplikacija/GpsMobilnaAplikacija/GpsMobilnaAplikacija)
* Korišten je boostrap koji automatski prilagođava veličinu ekrana za datu rezoluciju. Korišteno unutar cijelog projekta, a na linku iznad se može vidjeti i korištenje GPS-a unutar mobilne aplikacije.
* Web servis koji se pruža je također kroz ticksApiController (https://github.com/ooad-2015-2016/0xDEADC0DE/blob/master/fleet-tracker/fleet-tracker/Controllers/TicksApiController.cs)

  ![0xdeadc0de](https://raw.githubusercontent.com/ooad-2015-2016/0xDEADC0DE/master/deadcode.png)
