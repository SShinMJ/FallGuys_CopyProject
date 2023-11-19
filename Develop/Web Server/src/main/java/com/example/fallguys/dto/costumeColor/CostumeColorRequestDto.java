package com.example.fallguys.dto.costumeColor;

import com.example.fallguys.domain.CostumeColor;
import com.example.fallguys.domain.User;
import com.example.fallguys.domain.UserCostumeColor;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
public class CostumeColorRequestDto {
    private User user;
    private CostumeColor costumeColor;
    private boolean isOwn;

    public UserCostumeColor toEntity() {
        return UserCostumeColor.builder()
                .user(user)
                .costumeColor(costumeColor)
                .isOwn(isOwn)
                .build();
    }
}
