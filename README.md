**Starlight** is an experimental Natural Language Processing Engine built with the ML.NET framework. It processes requests based on a Machine Learning Dataset, which comes with the project and is editable by the user.



Example:

**Query:** *Wake me up at 10:30 AM* 

**Current date/time:** 9-nov-2019, 01:13 PM

**Output:**

```json
{
  "query": "wake me up at 10:30",
  "intents": [
    {
      "intent": "addAlarm",
      "score": 0.93108803
    },
    {
      "intent": "showWeather",
      "score": 0.5757521
    },
    {
      "intent": "addReminder",
      "score": 0.26036647
    }
  ],
  "entities": {
    "entity": "tomorrow",
    "type": "date",
    "startIndex": 14,
    "endIndex": 18,
    "date": "2019-11-10",
    "time": "10:30 AM"
  }
}
```

