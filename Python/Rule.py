class Rule():
    def __init__(self):
        self.rules = dict()

    def set_new_rule(self, color_a, color_b, g):
        self.rules[color_a, color_b] = g