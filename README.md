
# 📈BugFreeInvest - Software Quality Engineering Project

This project implements a high-integrity investment domain featuring Trader, Order, and Stock models, managed by an OrderService for core business logic. The codebase utilizes modern C# 12 patterns to handle complex financial rules. To demonstrate the power of automated enforcement, the project includes intentional "Quality Gate" configurations that trigger failures in the CI/CD pipeline, showcasing how strict engineering standards prevent technical debt from accumulating.

# Architecture

The solution follows a clean, decoupled architecture:

System.Domain: Contains the core production logic. It utilizes Guard Clauses and Switch Expressions to maintain low cyclomatic complexity. The OrderService uses constructor-based dependency injection to ensure test isolation.

System.Tests.Unit: Implements white-box testing with NUnit. It covers boundary value analysis and utilizes the Moq framework to isolate service logic from external dependencies.

System.Tests.BDD: Provides black-box testing through Gherkin scenarios using Reqnroll, focusing on user-centric investment flows.

# Testing Strategy
Boundary Value Analysis

The project focuses heavily on the ExecuteOrder and DeductBalance methods. Tests examine critical financial boundaries: zero-balance triggers, exact-cost orders, and insufficient fund scenarios. This ensures that the investment logic remains robust at the edges where standard tests often fail.

Test Isolation with Mocking
The OrderService interacts with an INotificationService. By using Moq, the test suite verifies that notifications are sent only upon successful order execution, proving the components are correctly decoupled and interacting as designed.

Behavior-Driven Development (BDD)
Using Reqnroll, business requirements are translated into executable Gherkin scenarios. This ensures that "When a trader places an order," the system behavior matches the business expectations, serving as living documentation for the project.

# Quality Metrics
The project achieves an elite 96.3% code coverage (Coverlet), with a 90.9% condition coverage, ensuring nearly every logical branch is exercised.

Mutation testing with Stryker.NET equaled a high-tier 75.00% mutation score. This 21.3% "gap" between coverage and mutation score is a key academic insight, demonstrating that while code is executed, mutation testing reveals where assertions need to be more "surgical" to catch subtle logic changes (e.g., boundary shifts in financial calculations).

Order.cs: 81.82% mutation score (High integrity validation logic).

OrderService.cs: 72.00% mutation score (Robust service-layer logic).

Stock.cs: 100% mutation score (Perfect logical verification).

# Static Analysis & Quality Gates
SonarQube is integrated to detect code smells and security risks.

Intentional Failure: The Quality Gate is configured to fail on specific code smells (such as non-grouped assertions) to demonstrate the enforcement of "Clean Test" principles.

Complexity: The DetermineOrderRiskLevel method uses C# 12 switch expressions to keep Cyclomatic Complexity low, allowing for a "Hand-Calculated CC vs. Automated CC" comparison during evaluation.

# Continuous Integration (CI/CD)
A GitHub Actions pipeline is triggered on every push. It performs:

Security Audit: Runs dotnet list package --vulnerable to check the supply chain.

Automated Build: Compiles the solution in Release mode.

Testing & Coverage: Executes all 18 tests and generates OpenCover reports.

Quality Gate Enforcement: Synchronizes with a self-hosted SonarQube runner, blocking the workflow if quality standards are not met.

# Technology Stack
- .NET 9.0 / C# 12

- NUnit 4 for Unit Testing

- Reqnroll for BDD

- Moq for Mocking/Isolation

- Stryker.NET for Mutation Testing

- SonarQube for Static Analysis

- GitHub Actions for CI/CD

# Key Insights
Coverage vs. Mutation: The distinction between 96.3% coverage (quantitative) and 75.0% mutation (qualitative) highlights the depth of the testing suite.

Secure-by-Design: The replacement of System.Random with RandomNumberGenerator demonstrates a professional awareness of cryptographic security in financial systems.

Modern Refactoring: Moving from nested if-else to Guard Clauses significantly improved the project’s maintainability rating.

# Project Status
All 18 tests pass. * Overall Coverage: 96.3%

Mutation Score: 75.00%

Quality Gate: FAILED (Intentional)

The project successfully demonstrates professional software quality engineering practices, rigorous testing metrics, and automated enforcement mechanisms suitable for academic evaluation.