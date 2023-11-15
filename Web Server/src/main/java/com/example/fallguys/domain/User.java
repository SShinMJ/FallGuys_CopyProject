package com.example.fallguys.domain;

import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import jakarta.persistence.*;
import java.util.Collection;

@Tag(name = "회원 정보", description = "식별자, 아이디, 비밀번호 등 회원 정보를 가진 Class")
@Entity(name = "user")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "User")
public class User implements UserDetails {

    @Schema(name = "식별자")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "user_number")
    private Long userNumber;

    @Schema(name = "아이디")
    @Column(name = "user_id", nullable = false, unique = true,  length = 40)
    private String userId;

    @Schema(name = "비밀번호")
    @Column(name = "user_password", nullable = false)
    private String userPassword;

    @Schema(name = "별명")
    @Column(name = "user_nickname", nullable = false,  length = 20)
    private String userNickname;

    @Schema(name = "쿠도스")
    @Column(name = "user_kudos", nullable = false)
    @ColumnDefault("0")
    private int userKudos;

    @Schema(name = "현재 코스튬 색")
    @Column(name = "user_costume_color", nullable = false)
    @ColumnDefault("0")
    private int userCostumeColor;


    @Builder
    public User (String userNickname, String userId, String userPassword) {
        this.userNickname = userNickname;
        this.userId = userId;
        this.userPassword = userPassword;
    }

    //해당 유저의 권한 목록
    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return null;
    }

    //비밀번호
    @Override
    public String getPassword() {
        return this.userPassword;
    }

    //아이디
    @Override
    public String getUsername() {
        return this.userId;
    }

    //계정 만료 여부
    //  true : 만료 안됨
    //  false : 만료
    @Override
    public boolean isAccountNonExpired() {
        return true;
    }

    //계정 잠김 여부
    //  true : 잠기지 않음
    //  false : 잠김
    @Override
    public boolean isAccountNonLocked() {
        return true;
    }

    //비밀번호 만료 여부
    //  true : 만료 안됨
    //  false : 만료
    @Override
    public boolean isCredentialsNonExpired() {
        return true;
    }

    //사용자 활성화 여부
    //  ture : 활성화
    //  false : 비활성화
    @Override
    public boolean isEnabled() {
        return true;
    }
}
