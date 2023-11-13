package com.example.fallguys.domain;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.*;
import org.hibernate.annotations.ColumnDefault;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import javax.persistence.*;
import java.util.Collection;

@ApiModel(value = "회원 정보", description = "아이디, 이메일, 비밀번호 등 회원 정보를 가진 Class")
@Entity(name = "user")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "USER")
public class User implements UserDetails {

    @ApiModelProperty(value = "식별자")
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "user_number")
    private Long userNumber;

    @ApiModelProperty(value = "이메일")
    @Column(name = "user_id", nullable = false, unique = true,  length = 40)
    private String userId;

    @ApiModelProperty(value = "비밀번호")
    @Column(name = "user_password", nullable = false)
    private String userPassword;

    @ApiModelProperty(value = "별명")
    @Column(name = "user_nickname", nullable = false,  length = 20)
    private String userNickname;

    @ApiModelProperty(value = "역할")
    @Column(name = "role", nullable = false)
    @ColumnDefault("false")
    private boolean role;

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

    //이메일
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