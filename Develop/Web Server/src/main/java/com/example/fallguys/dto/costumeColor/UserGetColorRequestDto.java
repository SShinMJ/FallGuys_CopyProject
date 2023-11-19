package com.example.fallguys.dto.costumeColor;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
@AllArgsConstructor
public class UserGetColorRequestDto {
    private Long costumeColorNumber;
    private int costumeColorCost;
}
