# The goal

For now I wanted to learn spanish in an effective way.
The idea was: find the most **valuable** words.

### Ngram files

Those are Ngrams extracted after cleaning the JSONs from OPUS (Open Subtitles)
for spanish (ES-es). I also removed some words and the 100 most common words.

 - 2-Grams-100000.csv
 - 3-Grams-100000.csv
 - 4-Grams-100000.csv

Each file contains the top most frequent N-Grams sorted by frequency in the corpus.

## Cleaning

### Cleaning characters

 - Special characters are all removed
 - OPUS has bad UTF-8 encoding, I try to convert, if that fails I discard a word
 - Some bad UTF-8 chars were probably punctuations (i.e: ... )
 - If bad UTF8 are found I discard entire word from sentence
 - The discarded word is only whitespace delimited (because bad punctuation can unite 2 words)
 - Other words are also punctuation delimited

### Cleaning words
 
 - Some words are excluded manually (i.e. "myspace", "www")
 - Some words are the most common 4000 english words (too much english content in OPUS)
 - Some englih words are not removed ( spanish people use "computer" word i.e.)
 - Phonetic only words are removed "eh eh" "ah"
 - Some english abbreviations added manually to be removed ("i" "t" "m")

### Sentence cleaning

 - Sentences are delimited only by ;:.!? characters
 - Comma and reversed !? are not delimiters
 - Ngrams are extracted only from withing sentences (no boundaries cross)

## Intent

Get NGrams of semantically linked words after removing noise and some cleanup.
Those NGrams in theory hide the statistics of cluster of words that are
convenient to learn, in order to help the:

**Ability to create sentences in Spanish** 

These NGrams still need to be processed, but it will be faster and easier
that waiting my PC digesting the UPUS Spanish corpus 2 hours.

## Algorithm

For now my idea is to use Ngrams as edges of a graph, the next 
node to be traveled should be random using as factors:

 - number of outgoing edges
 - frequency of the word among all Ngrams
 - frequency of that Ngram

The travel will use a Random Walk. Most traversed words
will get a greater score and after some time I have to cut
on scores.

 - Starting word is a seed
 - We can used multiple seeds (in example 1 food, 1 emotion and so on)
 - each seed will form a cluster
 - probably I need 1 big cluster plus some smaller clusters
 - If 2 clusters touch, maybe is a good idea to reseed from touchpoint.
 - I keep expanding until I reach desired number of words (i.e. 1000)
