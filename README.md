# memos-coding-tasks

Řešení úloh z technického zadání – zaměřeno na práci s daty, REST API a základní logiku nad grafy v jazyce C#.

## 🔧 Co repozitář obsahuje

Repozitář je rozdělený do tří samostatných úloh, každá v samostatném projektu:

---

### ✅ Task 01 – Duplicates

- Vygenerování milionu náhodných čísel v daném rozsahu.
- Nalezení nejčastějších duplikátů pomocí LINQ (`GroupBy`, `ToDictionary`, `OrderByDescending`).
- Výstup do konzole + zápis do souboru ve formátu CSV (TXT).
- Automatické otevření souboru v Notepadu.

---

### 🚀 Task 02 – Star Wars API

- Načítání veřejných dat z [SWAPI](https://swapi.dev/).
- Vyhledání všech obyvatel planety **Kashyyyk**.
- Získání všech lodí, které tito obyvatelé pilotovali.
- Výpis názvů těchto lodí do konzole.
- Ošetření výjimek při volání API.

Použito:
- `HttpClient`, `HttpClientHandler`
- `async/await`
- `JsonSerializer` (`System.Text.Json`)
- Parsování URL pro získání ID

---

### 🧠 Task 03 – Graph search

- Datová reprezentace posádky jako **stromová struktura** (graf).
- Možnost:
  - Vypsat všechny podřízené zvoleného člena.
  - Najít „cestu nákazy“ mezi členem posádky a kapitánem lodi (rekurze nahoru přes `Parent`).

Použito:
- Rekurze pro průchod stromem
- Vlastní datová struktura `CrewMember`
- Jednoduchý přístup pro sestavení cesty

---

## 💡 Poznámka

Všechny úlohy byly řešeny bez použití externích knihoven. Důraz byl kladen na přehlednost, čitelnost a samostatné zvládnutí zadání. Případné chyby v datech API (např. chybějící lodě) jsou ošetřeny.

---

## 👨‍💻 Autor

Josef Procházka  
