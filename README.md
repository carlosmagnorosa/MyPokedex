
# MyPokedex 

Pokedex API implemented with ASP.NET Core 5
This API exposes 3 endpoints.

Returns basic info about a given Pokemon
    
    /pokemon/{pokemonName}

Returns Pokemon with a fun description based on their habitat/legendary status

    /pokemon/translated/{pokemonName}


Out of the shelf ASP.NET Core Health endpoint middleware
      
      /health 

# To Build and Run

Download Docker (https://docs.docker.com/get-docker/)

Clone the code and in the Repository root run from the command line to build the docker image (it also runs the tests):
       
       docker build -t my-pokedex .

Now run the docker image:
         
       docker run -ti -p 8080:80 my-pokedex
      
Access the API in your Browser, Postman, Curl, etc from:
http://localhost:8080/
http://localhost:8080/Pokemon/ditto


# Some remarks
This API returns static data. 
An output cache was added to this API methods.
A MemoryCache was also added to the 2 external services.

For production, I would add improved logging, improved cache settings, global exception handling, a better Health check endpoint, and configure endpoints via Docker Environment variables
Depending on the importance of the service, we could also add metrics to verify error rates, cache hits/size, third party api errors.