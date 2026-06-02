
# declares which provider plugin to download

terraform {
  required_providers {
    docker = {
      source  = "kreuzwerker/docker"
      version = "~> 3.0"
    }
  }
}

#---------------------------------------------




#connects to your local Docker Desktop

provider "docker" {}

#---------------------------------------------




#tells Terraform about your image

resource "docker_image" "taskmanager" {
  name         = "taskmanager:latest"
  keep_locally = true
}

#---------------------------------------------




#creates and manages the container

resource "docker_container" "taskmanager" {
  name  = "taskapi"
  image = docker_image.taskmanager.image_id

  ports {
    internal = 8080
    external = 5287
  }

  restart = "unless-stopped"
}

#-----------------------------------------------