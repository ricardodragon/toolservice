# toolservice
management of tools 

## ToolType
this used for CRUD of tool type
- id: id tool type.
    - Integer
    - Ignored on Create, mandatory on the other methods
- name: name of tool type
    - String (50 characters)
    - Required
- description: description of tool type
    - String (100 characters)
- thingGroupIds: array with id thing group
    - int
- status: status of tool type

### JSON Example
```json
{
    "id": 1,
    "name": "Tipo",
    "description": "Criação tipo para as ferramentas update",
    "thingGroupIds": [
        2
    ],
    "status": "active"
}
```

## Url ToolType
* api/tooltype/{optional=startat}{optional=quantity}
    * GET: Return list tooltype
    * POST: Create tooltype
* api/tooltype/{id}
    * GET: Return tooltype
    * PUT: Update tooltype
        * Body: json tooltype
    * DELETE: inactive tooltype

## Tool
this used for CRUD of tool type
- id: id tool type.
    - Integer
    - Ignored on Create, mandatory on the other methods
- name: name of tool type
    - String (50 characters)
    - Required
- description: description of tool type
    - String (100 characters)
- lifeCycle: life time total
    - Double
    - Required
- currentLife: life consumed of tool
    - Double
- unitOfMeasurement: unit of measurement of life
    - String
    - Required
- typeId: id of tool type
    - Integer
    - Required
- typeName: name of tool type 
    - String
    - used only get (informative)
- status: status of tool

### JSON Example
```json
{
    "id": 1,
    "name": "Ferramenta",
    "description": "TESTE ferramenta",
    "lifeCycle": 100,
    "currentLife": 0,
    "unitOfMeasurement": "minute",
    "typeId": 1,
    "typeName": "Tipo",
    "status": "active"
}
```

## Url Tool
* api/tool/{optional=startat}{optional=quantity}
    * GET: Return list tool
    * POST: Create tool
* api/tool/{id}
    * GET: Return tool
    * PUT: Update tool
        * Body: json tool
    * DELETE: inactive tool


