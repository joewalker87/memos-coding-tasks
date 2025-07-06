# memos-coding-tasks

Solutions to technical assignments – focused on working with data, REST APIs, and basic graph logic in C#.

## 🔧 What the repository contains

The repository is divided into three standalone tasks, each in a separate project:

---

### ✅ Task 01 – Duplicates

- Generate one million random numbers within a given range.
- Find the most frequent duplicates using LINQ (`GroupBy`, `ToDictionary`, `OrderByDescending`).
- Output to console + save results to a CSV (TXT) file.
- Automatically open the file in Notepad.

---

### 🚀 Task 02 – Star Wars API

- Load public data from [SWAPI](https://swapi.dev/).
- Find all inhabitants of the planet **Kashyyyk**.
- Get all starships piloted by these inhabitants.
- Print ship names to the console.
- Handle exceptions during API calls.

Used:
- `HttpClient`, `HttpClientHandler`
- `async/await`
- `JsonSerializer` (`System.Text.Json`)
- Parse URL to extract ID

---

### 🧠 Task 03 – Graph search

- Represent crew data as a **tree structure** (graph).
- Features:
  - Print all subordinates of a selected member.
  - Find the “infection path” between a crew member and the ship’s captain (recursive upward via `Parent`).

Used:
- Recursion for tree traversal
- Custom data structure `CrewMember`
- Simple logic to build the path

---

## 💡 Note

All tasks were solved without using external libraries. Emphasis was placed on clarity, readability, and independent problem-solving. Possible API data issues (e.g., missing ships) are handled.

---

## 👨‍💻 Author

Josef Procházka
