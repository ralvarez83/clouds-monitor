# Cloud Monitor Application

Esta aplicación es un proyecto de aprendizaje diseñado para explorar la arquitectura de microservicios. Gestiona y recupera información de backups de máquinas virtuales en Azure utilizando diversos componentes y tecnologías.

Para su implementación se ha usado la metodología TDD que nos indica que hemos de hacer 1 prueba, después hacer que pase y a continuación refactorizar. Seguiremos con la siguiente prueba.

## Aprendizajes

- He tenido que reestructurarme en la cabeza el patrón criteria, definía mal los filtros.
- He aprendido la nueva manera de usar Entity Framework (desde la definición en código de las entidades)
- A usar MongoDB, cuando me veía que había acabado he tenido que crear una clase de Entidad para poder soportar adecuadamente una clave primaria (¡No sabía que había claves primarias en base de datos no SQL!)
- Me ha costado mucho entender los conceptos de Exchange y Colas de RabbitMQ, ahora ya tiene sentido!!

## Estado del desarrollo

Componentes:

- Front: _To do_
- API: _To do_
- LastBackupSuscriber: _To do_
- CloudRecoveryLastBackups: **DONE**

## Tecnologías Utilizadas

- .NET Core 8 (para las aplicaciones de consola y API)
- MongoDB
- RabbitMQ
- Azure

## Arquitectura

Para crear esta aplicación se han usado las siguientes arquitecturas y patrones de diseño:

- Arquitectura Hexagonal
- Patrón de diseño DDD
- CQRS
- Patrón Repository
- Patrón Criteria o Specification

La aplicación consta de los siguientes componentes principales:

1. **Aplicación Principal**

   - Frontend: Interfaz de usuario de la aplicación
   - API: Maneja las solicitudes del frontend
   - LastBackupSuscriber: Aplicación de consola que se suscribe a y procesa actualizaciones de backups

2. **Base de Datos**

   - MongoDB: Almacena datos de la aplicación

3. **Mensajería**

   - RabbitMQ: Sistema de mensajería para la comunicación entre servicios

4. **Servicios en la Nube**
   - CloudRecoveryLastBackups: Aplicación de consola que recupera información de backups de Azure
   - Azure: Plataforma donde se almacenan los backups de las máquinas virtuales

![Diagrama de la arquitectura](./scheme/Cloud-monitor-scheme.png)

## Funcionamiento

1. CloudRecoveryLastBackups recupera la información de los últimos backups de las máquinas de un Tenant en Azure.
2. Esta información se transmite a través de RabbitMQ.
3. LastBackupSuscribers se suscribe a estos datos de backups a través de RabbitMQ.
4. LastBackupSuscribers procesa los datos recibidos, realizando las operaciones necesarias con la información de los backups.
5. La API accede a los datos procesados según sea necesario.
6. El Frontend muestra la información procesada al usuario.

## Configuración y Uso

Para configurar y ejecutar la aplicación, sigue estos pasos:

1. Clona el repositorio en tu máquina local.
2. Asegúrate de tener instaladas todas las dependencias necesarias (.NET, MongoDB, RabbitMQ).
3. Crea un archivo `appsettings.json` en el directorio raíz del proyecto con la siguiente configuración:

```json
{
  "EnvironmentVariables": [
    {
      "key": "AZURE_CLIENT_ID",
      "value": ""
    },
    {
      "key": "AZURE_TENANT_ID",
      "value": ""
    },
    {
      "key": "AZURE_CLIENT_SECRET",
      "value": ""
    }
  ],
  "Suscriptions": [
    {
      "Id": "",
      "ResourcesGroups": [
        {
          "Name": "",
          "Vaults": [
            {
              "Name": ""
            }
          ]
        }
      ]
    },
    {
      "Id": "",
      "ResourcesGroups": [
        {
          "Name": "",
          "Vaults": [
            {
              "Name": ""
            }
          ]
        }
      ]
    }
  ],
  "MongoDBSettings": {
    "MongoDBURI": "",
    "DatabaseName": ""
  },
  "RabbitMQ": {
    "HostName": "",
    "UserName": "",
    "Password": "",
    "Port": 0,
    "Exchange": {
      "Name": "",
      "Subscribers": [
        {
          "QueuName": "",
          "EventName": ""
        }
      ]
    }
  }
}
```

4. Completa los campos vacíos en el archivo `appsettings.json` con tus propios valores:

   - Credenciales de Azure (AZURE_CLIENT_ID, AZURE_TENANT_ID, AZURE_CLIENT_SECRET)
   - Detalles de las suscripciones, grupos de recursos y vault de Azure
   - Configuración de MongoDB (URI y nombre de la base de datos)
   - Configuración de RabbitMQ (host, usuario, contraseña, puerto, etc.)

5. [Aquí puedes agregar instrucciones adicionales sobre cómo ejecutar la aplicación]

Asegúrate de no compartir tus credenciales o información sensible. El archivo `appsettings.json` debe estar incluido en el `.gitignore` para evitar que se suba al repositorio.

## Contribución

Este proyecto no admite contribuciones de momento dado que su implementación es puramente didactica.

## Licencia

[GNU GENERAL PUBLIC LICENSE](./LICENSE)
