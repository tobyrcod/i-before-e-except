directory = "Wherever you have saved words_alpha.txt"
allWordsFile = "/words_alpha.txt"
ruleFollowerFile = "/rule_follower_words.txt"
ruleBreakerFile = "/rule_breaker_words.txt"
ignoreFile = "/ignore_words.txt"

file = open(directory + ruleFollowerFile, 'r')
rule_followers = file.read().splitlines()
file.close()

file = open(directory + ruleBreakerFile, 'r')
rule_breakers = file.read().splitlines()
file.close()

both_words = list()
for b_word in rule_breakers:
    if b_word in rule_followers:
        both_words.append(b_word)


