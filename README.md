**Starlight** is a Natural Language Processing Engine built with the ML.NET framework. It processes requests using binary classification based on a Machine Learning Dataset.

The included Dataset is task-oriented and has entity extraction built in, the user can edit or replace it according to the intended use case.



Example:

**Query:** *Wake me up at 10:30*

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

