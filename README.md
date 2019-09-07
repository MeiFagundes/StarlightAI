**Starlight** is an experimental Natural Language Processing (NLP) Engine built with the ML.NET framework. It processes requests based on a Machine Learning Dataset, which comes with the project and is editable by the user.



Example:

**Query:** *Remind me to work on my project tomorrow*

**Output:**

```
{
  "query": "Remind me to work on my project tomorrow",
  "intents": [
    {
      "intent": "addReminder",
      "score": 0.9951776
    },
    {
      "intent": "showWeather",
      "score": 0.477052838
    },
    {
      "intent": "showNews",
      "score": 0.008260157
    }
  ]
}
```

