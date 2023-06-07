from random import Random
import pygame, sys
from Universe import Universe
from game import Game
from Color import Color
from Rule import Rule
import Particle

class Particle_life(Game):
    def __init__(self, width, height, title, tick):
        super().__init__(width=width, height=height, title=title, tick=tick)
        self.random = Random()
        self.particles = []
        self.rules = Rule()

    def new(self):
        for _ in range(75):
            #(255, 255, 255)
            self.particles.append(Particle.Particle(self.random.randrange(0, self.width), self.random.randrange(0, self.height), Color.WHITE))

        for _ in range(75):
            #(50, 255, 255)
            self.particles.append(Particle.Particle(self.random.randrange(0, self.width), self.random.randrange(0, self.height), Color.BLUE))

        for _ in range(75):
            #(255, 50, 255)
            self.particles.append(Particle.Particle(self.random.randrange(0, self.width), self.random.randrange(0, self.height), Color.PINK))

        self.rules.set_new_rule(Color.WHITE, Color.WHITE, -5)
        self.rules.set_new_rule(Color.WHITE, Color.BLUE, 5)
        self.rules.set_new_rule(Color.WHITE, Color.PINK, -3)

        self.rules.set_new_rule(Color.BLUE, Color.WHITE, -1)
        self.rules.set_new_rule(Color.BLUE, Color.BLUE, 0)
        self.rules.set_new_rule(Color.BLUE, Color.PINK, -3)

        self.rules.set_new_rule(Color.PINK, Color.WHITE, 3)
        self.rules.set_new_rule(Color.PINK, Color.BLUE, 3)
        self.rules.set_new_rule(Color.PINK, Color.PINK, 3)
        
        self.universe = Universe(self.width, self.rules)
        self.run()

    def events(self):
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                sys.exit()

            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_c:
                    pass

    def draw(self):
        self.screen.fill((0, 0, 0))
        self.universe.update(self.particles)
                  
        for i in range(len(self.particles)):
            pygame.draw.circle(self.screen, self.particles[i].color.value, (self.particles[i].x, self.particles[i].y), 3, 0)

        print(self.clock.get_fps())
        pygame.display.update()

def main():
    particle_life = Particle_life(700, 700, "ParticleLife", 240)
    particle_life.new()

if __name__ == "__main__":
    main()