
![Logo](https://raw.githubusercontent.com/UkeHa/SalonikiAlexaSkill/master/SalonikiAlexa/Resources/amphore.png)


# Saloniki Alexa Skill

This skill shows off a simple menu generator with a appetizer, main course and drink for a local restaurant.

## Run "Locally"

Clone the project

```bash
  git clone https://github.com/UkeHa/SalonikiAlexaSkill.git
```

Create aws lambda function with dotnet 6 and add Alexa as trigger. Add skill id to the trigger (you can find it under Endpoint in the Alexa skill)

```bash
  https://eu-central-1.console.aws.amazon.com/lambda/home?region=eu-central-1#/functions
```

Copy the Function-ARN-code and create Alexa Skill. Add the arn-code in your alexa skill under Endpoint

```bash
  https://developer.amazon.com/alexa/console/ask
```

Push code from terminal (inside the cloned repository folder) to lambda

```bash
  dotnet lambda deploy-function SalonikiApen
```

## Authors

- [@UkeHa](https://github.com/UkeHa/)
- OpenApi DALL-E 2 created the logo