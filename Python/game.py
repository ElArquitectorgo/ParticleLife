import pygame

class Game:
    def __init__(self, width: int, height: int, title: str, tick: int):
        pygame.init()
        pygame.mixer.init()
        pygame.font.init()

        self.width = width
        self.height = height
        self.screen = pygame.display.set_mode((width, height))
        pygame.display.set_caption(title)
        self.clock = pygame.time.Clock()
        self.tick = tick

    def run(self):
        while True:
            self.clock.tick(self.tick)
            self.events()
            self.draw()