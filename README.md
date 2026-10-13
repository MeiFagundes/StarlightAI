# Starlight

Starlight is a C# natural-language intent classification project built with ML.NET. It trains a binary classifier for each intent from the corresponding text file in `Dataset/` and scores an input utterance against those intents. Its JSON response includes intent scores and entity fields.

The included datasets cover intents such as alarms, reminders, weather, news, music, calls, and small talk. The repository contains date and time extraction classes for alarm and reminder requests. The console entry point prompts for an utterance and prints the result.

## Example

The following example is preserved from the original README, using its November 9, 2019 date context.

**Query:** *Wake me up at 10:30*

**Current date/time in the example:** November 9, 2019, 1:13 PM

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

## Technologies

C#, .NET Core 3.0 and ML.NET 1.3.1.
