# The goal

For now I wanted to learn spanish in an effective way.
The idea was: find the most **valuable** words.

### Ngram files

Those are Ngrams extracted after cleaning the JSONs from OPUS (Open Subtitles)
for spanish (ES-es). 

 - 2-Grams-100000.csv
 - 3-Grams-100000.csv
 - 4-Grams-100000.csv

Each file contains the top 100.000 most frequent N-Grams sorted by frequency in the corpus.

**CSV Format:**
Count;Insertions;Ngram 

- **Count** column: Estimated frequency
- **Insertions** column: Counted frequency
- **NGram** column: the ngram (UTF8)

**Rationale**

- Since the PC has limited RAM Ngrams are stored with a SpaceSaving priority queue
- Ngrams that have very few insertions (1-2 or maybe 3) probably have a lower real count
- If Count == Insertions => Then the Count is real for that NGram

## Cleaning

### Cleaning characters

 - Special characters are all removed
 - OPUS has bad UTF-8 encoding, I try to convert, if that fails I discard a word
 - Some bad UTF-8 chars were probably punctuations (i.e: ... )
 - If bad UTF8 are found I discard entire word from sentence
 - The discarded word is only whitespace delimited (because bad punctuation can unite 2 words)
 - Other words are also punctuation delimited

### Cleaning words

 - Most common words are linked too much to everything. I removed the top 100 ("el", "yo", ...)
 - Some words are excluded manually (i.e. "myspace", "www")
 - Some words are the most common 4000 english words (too much english content in OPUS)
 - Some english words are not removed ( spanish people use "computer" word i.e.)
 - Phonetic only words are removed "eh eh" "ah"
 - Some english abbreviations added manually to be removed ("i" "t" "m")
 - **Note:** Some Ngrams are specific to only 1 tv show... reduce them to 1?
 - Duplicate words are not retained since we are building a dictionary, i.e "bien bien" > discarded

### Sentence cleaning

 - Sentences are delimited only by ;:.!? characters
 - Comma and reversed !? are not delimiters
 - Ngrams are extracted only from withing sentences (no boundaries cross)

## After Cleaning

 - + **210 millions** words
 - **104 millions** 2-Grams
 - **64 millions** 3-Grams
 - **40 millions** 4-Grams
 
## Intent

Get NGrams of semantically linked words after removing noise and some cleanup.
Those NGrams in theory hide the statistics of clusters of words that are
convenient to learn, in order to help the:

**Ability to create sentences in Spanish** 

These NGrams still need to be processed, but it will be faster and easier
than waiting my PC digesting the OPUS Spanish corpus 2 hours.

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
 - If 2 clusters touch, reseed from touchpoint?
 - I keep expanding until I reach desired number of words (i.e. 1000)
 
### Unsolved problem

The algorithm works well, and at a first glance, it seems the order
of words makes sense.
However the words are not necessarily linked. That's because starting
from one seed expand the neighbour words, and neighbours are not 
necessarily related.
The Idea is to amend that and create chains so words are sorted
by usability in a chain. The dictionary now is already probably better
than a list of words sorted by frequency.

###

Some words I expect to find once the algo work:

todavía

aunque

casi

apenas

igual

seguro

quizá

además

siempre

todavía

mientras

dentro

fuera

acabar

dejar

faltar
