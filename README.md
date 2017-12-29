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
- serialNumber: serial number of tool
    - String (100 characters)
- code: code of tool
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
    "serialNumber": null,
    "code": null,
    "lifeCycle": 100,
    "currentLife": 0,
    "unitOfMeasurement": "minute",
    "typeId": 1,
    "typeName": "Tipo",
    "status": "active"
}
```

## Url Tool
* api/tool/{optional=startat}{optional=quantity}{optional=orderField}{optional=order}{optional=fieldFilter}{optional=fieldValue}
    * GET: Return list tool
        * startat: represent where the list starts t the database (Default=0)
        * quantity: number of resuls in the query (Default=50)
        * orderField: Field in which the list will be order by (Possible Values:
            Name,Description,SerialNumber,Code,UnitOfMeasurement,TypeName
            Status)(Default=id)
        * order: Represent the order of the listing (Possible Values: ascending,
            descending)(Default=Ascending)
        * fieldFilter: represents the field that will be seached (Possible Values:
            Name,Description,SerialNumber,Code,UnitOfMeasurement,TypeName
            Status) (Default=null)
        * fieldValue: represents de valued searched on the field (Default=null)
    * POST: Create tool
* api/tool/{id}
    * GET: Return tool
    * PUT: Update tool
        * Body: json tool
    * DELETE: inactive tool


