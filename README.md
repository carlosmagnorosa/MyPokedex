
# MyPokedex 

Pokedex API implemented with ASP.NET Core 5

This API exposes 3 endpoints.

Returns basic info about a given Pokemon:
    
    /pokemon/{pokemonName}

Returns Pokemon with a fun description based on their habitat/legendary status:

    /pokemon/translated/{pokemonName}


Out of the shelf ASP.NET Core Health endpoint middleware:
      
      /health 

# To Build and Run

Install Docker (https://docs.docker.com/get-docker/)

To build the docker image (building also runs the tests): run the following command from the root folder
       
       docker build -t my-pokedex .

Now run the docker image:
         
       docker run -ti -p 8080:80 my-pokedex
      
Access the API from your Browser, Postman, Curl, etc from:

http://localhost:8080/

example: http://localhost:8080/Pokemon/ditto


# Some remarks
This API returns static data. An output cache was added to two /pokemon API methods.

A MemoryCache was also added to the 2 external services.

For production, I would add improved logging, improved cache settings, global exception handling, a better Health check endpoint, and configure endpoints via Docker Environment variables

Depending on the importance of the service, we could also add metrics to verify error rates, cache hits/size, third party api errors.
