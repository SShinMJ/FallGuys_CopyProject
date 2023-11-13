package com.example.fallguys.dto.user;

import com.example.fallguys.domain.User;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
public class UserSignupRequestDto {
    private String userId;
    private String userPassword;
    private String userNickname;

    @Builder
    public UserSignupRequestDto(String userId, String userPassword, String userNickname) {
        this.userId = userId;
        this.userPassword = userPassword;
        this.userNickname = userNickname;
    }

    public User toEntity() {
        return User.builder()
                .userId(userId)
                .userPassword(userPassword)
                .userNickname(userNickname)
                .build();
    }
}
