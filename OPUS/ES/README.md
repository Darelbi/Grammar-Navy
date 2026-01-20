# The goal

For now I wanted to learn spanish in an effective way.
The idea was: find the most **valuable** words.

## Ngram files

Those are Ngrams extracted after cleaning the JSONs from OPUS (Open Subtitles)
for spanish (ES-es). I also removed some words and the 100 most common words.

2-Grams-50000.csv
3-Grams-100000.csv

### Cleaning

I had to do some fix on wrong UTF8 conversions. There were other dirty characters
but they were few. So any word that contains dirty characters is removed from a sentence
I recognize this may alter Ngrams on very limited cases! But beware.

Text is cleaned (code provided) by removing punctuation and non-unicode letters.

Sentences are split over ":;.!?" characters. Reversed ?! are not counted and
comma is not a delimiter.

N grams are computed within sentences. Because the goal is to make sentences.
Not to make dialogues. The assumption is that better sentences allows better
dialogues. I could be wrong of course!

But seems there are no similiar open source projects. If you have requests open
a ticket. It will probably be interesting for me anyway. Thanks

**Important**

I Still have to clean english words and phonetic things lik "sigh" "ah ah"
"he he"

### Intent

Get semantically near words, excluded most common words.
Ideally I can build from these a dictionary of words with which is
possible to build many sentences.

### Warning

High frequency does not necessarily mean high number of sentences
so I should try to balance heurisitcs:

- **Frequency of a word in Ngrams**
- **Number of Ngrams that use that word**
- Use a sort of random walk starting from seed/s
- The seeds can come from different domains (emotions/food etc)
- Some seeds must use LONG random walks
- Feed up each cluster up to X given words
- When dictionary reach desired size sort by heuristics.

### Good use

Ideally by running an algorithm on the two Ngram files I should be
able to create a dictionary effective for learning Spanish faster.

Extract semantically near words.


### Notes

Depending on our purposes we could keep as few 3-Grams as 4500
and 2-Grams as 30000. Or retain everthing. It's true that some
Ngrams may be rare, but they could give indirect information on
some of the words they contains.
