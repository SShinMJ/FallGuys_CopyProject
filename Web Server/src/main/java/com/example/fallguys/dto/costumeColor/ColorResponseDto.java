package com.example.fallguys.dto.costumeColor;

import com.example.fallguys.domain.CostumeColor;
import com.example.fallguys.domain.User;
import com.example.fallguys.domain.UserCostumeColor;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

import java.util.List;

@Getter
@AllArgsConstructor
public class ColorResponseDto {
    private final CostumeColor costumeColor;
    private final boolean isOwn;

    public ColorResponseDto(UserCostumeColor userCostumeColor) {
        this.costumeColor = userCostumeColor.getCostumeColor();
        this.isOwn = userCostumeColor.isOwn();
    }
}