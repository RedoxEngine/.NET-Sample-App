# .NET Sample App

A simple web app demonstrating a way to send and receive healthcare data with Redox.

This repo contains three projects: 
1. A model class generator that can be used to translate JSON schema files from [Redox Documentation](https://developer.redoxengine.com/data-models/schemas.zip) to C# classes.
2. A RestSharp-based client that houses the model classes and can be used to manage authorization keys and generate requests to the redox API.
3. An example website project that demonstrates potential uses of the API client. The app also provides an API that can be used to receive requests from Redox.

