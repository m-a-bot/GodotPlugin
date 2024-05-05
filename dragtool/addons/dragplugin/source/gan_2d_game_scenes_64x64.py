import torch
import torch.nn as nn
import torch.nn.functional as F
import random
from torchvision.utils import save_image

device = "cuda" if torch.cuda.is_available() else "cpu"

IMAGE_SIZE = 64
Z_DIM = 128

class Generator(nn.Module):
    
    def __init__(self, z_dim, img_size, img_channels=3):
        super().__init__()
        self.reg = 0
        self.img_size = img_size
        self.img_channels = img_channels

        feature = 128

        # 128 256
        # 256 512
        layers = []
        for i in range(3):
            layers.append(
                nn.Sequential(
                nn.Linear(feature, feature*2, bias=False),
                nn.Dropout(0.3, True),
                nn.BatchNorm1d(feature*2),
                nn.LeakyReLU(0.1, True)
                )
            )
            feature *= 2

        self.layers = nn.Sequential(*layers)

        self.init = nn.Sequential(
            nn.Linear(z_dim, 128, bias=False),
            nn.BatchNorm1d(128),
            nn.ReLU(True)
        )

        self.last = nn.Sequential(
                nn.Linear(1024, img_size * img_size * img_channels),
                nn.Tanh()
                )

    def forward(self, x):
        batch_size, *_ = x.shape
        # ortho_loss = orthogonal_reg(self.fc1.weight) + \
        #                 orthogonal_reg(self.fc2.weight) + \
        #                 orthogonal_reg(self.fc3.weight) + \
        #                 orthogonal_reg(self.fc4.weight)
        # self.reg = ortho_loss
        
        # b x z_dim x 1 x 1
        x = self.init(x)
        
        # b x 128 x 1 x 1
        x = self.layers(x)
        x = self.last(x)

        # b x img_size * img_size * 3 x 1 x 1
        return x.reshape(batch_size, self.img_channels, self.img_size, self.img_size)

import sys


path_project, seed = sys.argv[1].split()

# seed = sys.argv[1]


import os

# cwd = os.getcwd()

# print(cwd)

# print(path_project)

if __name__ == "__main__":
    # gen = Generator(Z_DIM, IMAGE_SIZE).to(device)

    # gen = torch.load("gen_stable.pth").to(device)

    z = torch.randn(1, 3, 64, 64).to(device)

    # fake = gen(z)

    save_image(z, path_project + "/test.png")

    print("test.png")

