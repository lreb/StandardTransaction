# Standard Transaction
Baseline for responses with more details in complex process

## Examples of responses

### Success
```json
{
  "Code": 1,
  "Success": true,
  "Fail": false,
  "Warning": false,
  "Message": "DI",
  "Data": {
    "Id": 1,
    "Value": "some"
  },
  "EventReference": "10",
  "MessageDetail": []
}
```
### Warning
```json
{
  "Code": 2,
  "Success": false,
  "Fail": false,
  "Warning": true,
  "Message": "WARNING",
  "Data": null,
  "EventReference": "12",
  "MessageDetail": [
    {
      "type": 0,
      "key": "INTERNAL_CODE",
      "message": "message",
      "messageDetail": "some detail"
    }
  ]
}
```
### Error
```json
{
  "Code": 0,
  "Success": false,
  "Fail": true,
  "Warning": false,
  "Message": "ERROR",
  "Data": null,
  "EventReference": "12",
  "MessageDetail": [
    {
      "type": 0,
      "key": "INTERNAL_CODE",
      "message": "Index was outside the bounds of the array.",
      "messageDetail": "some detail"
    }
  ]
}
```
