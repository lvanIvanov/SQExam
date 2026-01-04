Feature: Stock order execution

    Scenario: Successful buy order
        Given a trader with sufficient balance
        And a valid stock
        When the trader places a buy order
        Then the order should be executed

    Scenario: Order rejected due to invalid quantity
        Given a trader
        When the trader places an order with quantity 0
        Then the order should be rejected

    Scenario: Trader is notified on execution
        Given a trader and notification service
        When the order is executed
        Then the trader should receive a notification