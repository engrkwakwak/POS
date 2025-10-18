# Contribution Guidelines

Thank you for contributing to our project! To ensure the project is easy
to navigate, manage, and understand, we adhere to a set of guidelines
for creating GitHub issues and branches. Following these conventions
helps us streamline the development process and maintain a clear,
organized repository.

## Creating Issues

A well-written issue makes it easier to understand, prioritize, and
address the task at hand.

### Naming Convention

All issue titles must follow a specific format to clearly communicate
the nature of the work. This helps everyone quickly understand what an
issue is about just by looking at the title in the project board or
issues list.

**Format:**\
`type: A brief, imperative description of the task`

### Issue Types

The **type prefix** is mandatory and must be one of the following
keywords. This helps us categorize and filter issues effectively.

  --------------------------------------------------------------------------------------------------------------
  Prefix              Description                    Example
  ------------------- ------------------------------ -----------------------------------------------------------
  feat                A new feature or user-facing   `feat: Add endpoint to create purchase receipts`
                      functionality.                 

  bug                 A bug fix that corrects        `bug: Order total miscalculates when quantity is updated`
                      unintended behavior.           

  docs                Changes to documentation, such `docs: Update purchase order status definitions`
                      as the README or API specs.    

  refactor            A code change that neither     `refactor: Replace Quantity record with a class`
                      fixes a bug nor adds a         
                      feature.                       

  chore               Routine maintenance, build     `chore: Upgrade EF Core to the latest version`
                      process changes, or dependency 
                      updates.                       

  test                Adding new tests or improving  `test: Add unit tests for PurchaseReceipt invariants`
                      existing ones. No production   
                      code changes.                  
  --------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------

## Branching and Workflow

We follow a simplified **GitFlow** model. This means we have two
primary, long-living branches:

-   **main**: This branch contains production-ready code. Direct commits
    to `main` are forbidden.\
-   **develop**: This is the primary development branch. It serves as an
    integration branch for features and fixes. All new work starts here.

### Branch Naming Convention

To keep our branch structure clean and link work directly to issues, all
branch names must follow this format:

    type/pos-{issue-number}

-   **type**: Describes the category of work, based on the issue type.\
-   **pos-**: A short identifier for our project (Point of Sale).\
-   **{issue-number}**: The number of the GitHub issue this branch
    resolves.

Use the following mapping for the branch type:

  Issue Prefix   Branch Prefix
  -------------- ---------------
  feat           feature
  bug            bugfix
  docs           docs
  refactor       refactor
  chore          chore
  test           test

------------------------------------------------------------------------

### Workflow Steps

1.  Before starting work, ensure you have the latest version of the
    `develop` branch.\
2.  Create a new branch from `develop` using the naming convention
    above.\
3.  Commit your work to this branch.\
4.  When your work is complete, open a **Pull Request** to merge your
    branch back into `develop`.

------------------------------------------------------------------------

### Branch Naming Examples

-   An issue **#42** titled
    `feat: Add endpoint to get purchase receipts` should have a branch
    named:

        feature/pos-42

-   An issue **#58** titled `bug: Order total miscalculates` should have
    a branch named:

        bugfix/pos-58

-   An issue **#61** titled `docs: Update README` should have a branch
    named:

        docs/pos-61
