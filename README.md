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


