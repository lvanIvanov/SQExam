📈 BugFreeInvest - Quality & Security Framework
This repository demonstrates a Shift-Left approach to software quality and security, designed for the Agramkow stock trading ecosystem. It is built to comply with the Cyber Resilience Act (CRA) by integrating automated Quality Gates directly into the CI/CD pipeline.

🚀 Key Features & Tech Stack
Domain: C# .NET 8.0 Stock Trading Logic.

Behavior Driven Development (BDD): Reusable Gherkin features with SpecFlow/Reqnroll.

Mutation Testing: Stryker.NET to validate test suite effectiveness.

Static Analysis: SonarQube running in Docker for security and complexity audits.

Automation: GitHub Actions with a Self-Hosted Runner (configured for local analysis).

🛠 Quality Gate Components
1. Static Analysis (SonarQube)
   The project includes intentional "Smells" and "Vulnerabilities" to demonstrate the effectiveness of the Quality Gate:

SQL Injection: Vulnerable query concatenation in OrderService.cs.

Cognitive Complexity: High-branching logic in DetermineOrderRiskLevel (CC = 6).

Security Hotspots: Weak RNG (Random) used for sensitive IDs.

Maintainability: Public fields breaking encapsulation and empty catch blocks.

2. Mutation Testing (Stryker.NET)
   We don't just measure Code Coverage; we measure Test Strength.

Current Mutation Score: ~38.71%

Goal: Identifying "Survivor" mutants to strengthen assertions in the Domain layer.

🚦 How to Run
Local Analysis (Docker)
Start SonarQube:

Bash

docker-compose up -d
Run the Scanner:

Bash

dotnet sonarscanner begin /k:"BugFreeInvest" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="YOUR_TOKEN"
dotnet build
dotnet sonarscanner end /d:sonar.token="YOUR_TOKEN"
Mutation Testing
Bash

dotnet stryker --project System.Domain.csproj
📚 Academic Context (Exam Topics)
Topic 4.1 (Static Analysis): Using SonarQube to detect vulnerabilities and technical debt.

Topic 4.2 (Complexity): Measuring Cyclomatic Complexity to improve maintainability.

Topic 7 (Black-box vs White-box): BDD (Gherkin) vs Mutation-verified Unit Tests.

Cyber Resilience Act: Automating security checks to ensure software sustainability.

👨‍💻 Author
Ivan - Software Engineering Student

How to add this to GitHub:
In Rider, right-click your Solution.

Select New > File and name it README.md.

Paste the code above into the file.

Commit and Push: ```bash git add README.md git commit -m "Add professional README for exam" git push